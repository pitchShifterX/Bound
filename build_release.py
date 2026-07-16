#!/usr/bin/env python3
import os
import shutil
import subprocess
import tempfile
import zipfile
from pathlib import Path

def build_release(mod_name="Bound", output_name="Bound.zip"):
    current_working_dir = Path(__file__).parent.resolve()
    mod_install_dir = current_working_dir / "Mods" / mod_name / "Install"
    
    if not mod_install_dir.exists():
        print(f"Error: {mod_install_dir} not found")
        return False
    
    with tempfile.TemporaryDirectory() as temp_dir:
        temp_path = Path(temp_dir)
        print(f"Building in temporary directory: {temp_path}")
        
        mods_output_dir = temp_path / "mods" / mod_name
        mods_output_dir.mkdir(parents=True, exist_ok=True)
        
        print(f"Copying mod files from {mod_install_dir}...")
        for item in mod_install_dir.iterdir():
            if item.is_dir():
                shutil.copytree(item, mods_output_dir / item.name, dirs_exist_ok=True)
            else:
                shutil.copy2(item, mods_output_dir / item.name)
        
        print("Building binary with dotnet...")
        
        build_cmd = [
            "dotnet", "build",
            "-c", "Release",
            f"-o", str(temp_path / "build")
        ]
        
        print(f"Running: {' '.join(build_cmd)}")
        result = subprocess.run(build_cmd, cwd=str(current_working_dir))

        if result.returncode != 0:
            print(f"Build failed with return code: {result.returncode}")
            return False

        print("Build successful!")
        
        build_output = temp_path / "build"

        print("Organizing package files...")

        for item in build_output.rglob("*"):
            if item.is_file():
                if item.suffix == ".pdb":
                    continue
                
                rel_path = item.relative_to(build_output)
                dest_path = temp_path / rel_path
                
                dest_path.parent.mkdir(parents=True, exist_ok=True)
                shutil.copy2(item, dest_path)

        shutil.rmtree(build_output)
        
        output_zip = current_working_dir / output_name
        print(f"Creating release: {output_zip}")
        
        with zipfile.ZipFile(output_zip, 'w', zipfile.ZIP_DEFLATED) as zf:
            for root, dirs, files in os.walk(temp_path):
                for file in files:
                    file_path = Path(root) / file
                    arcname = file_path.relative_to(temp_path)
                    zf.write(file_path, arcname)
        
        print(f"Release created: {output_zip}")
        
        print("\nMod package contents:")
        with zipfile.ZipFile(output_zip, 'r') as zf:
            for filename in sorted(zf.namelist()):
                print(f"  {filename}")
        
        return True

if __name__ == "__main__":
    import sys
    
    mod_name = sys.argv[1] if len(sys.argv) > 1 else "Bound"
    output_name = sys.argv[2] if len(sys.argv) > 2 else f"{mod_name}.zip"
    
    success = build_release(mod_name, output_name)
    exit(0 if success else 1)