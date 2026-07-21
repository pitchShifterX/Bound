using System.Numerics;
using GameEngine.Utilities;

namespace GameEngine.Render
{
    public class Camera : ICameraController, ICameraView
    {
        private Vector2 _position;
        private float _zoom = 2.0f;
        private float _movementSpeed = 300f;
        private Vector2 _movementDirection = Vector2.Zero;

        private int _viewportWidth;
        private int _viewportHeight;

        private int _worldPixelWidth;
        private int _worldPixelHeight;

        public int ViewportWidth => _viewportWidth;
        public int ViewportHeight => _viewportHeight;

        public Vector2 WorldPosition => _position;
        public float Zoom => _zoom;
        public float MovementSpeed => _movementSpeed;

        public Rectangle<float> VisibleWorldBounds
        {
            get
            {
                float width = ViewportWidth / Zoom;
                float height = ViewportHeight / Zoom;

                return new(
                    X: WorldPosition.X - width / 2,
                    Y: WorldPosition.Y - height / 2,
                    width,
                    height
                );
            }
        }

        public Camera(
            int viewportWidth, 
            int viewportHeight, 
            int worldTileWidth, 
            int worldTileHeight
        )
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _worldPixelWidth = worldTileWidth * Constants.TileSize;
            _worldPixelHeight = worldTileHeight * Constants.TileSize;

            // center camera on map
            // will need to update this when map locations are integrated
            // so users can define where camera starts
            _position = new Vector2(_worldPixelWidth / 2f, _worldPixelHeight / 2f);

            clampPosition();
        }

        public Camera(
            Vector2<int> resolution,
            Vector2<int> mapSize
        ) : this(resolution.x, resolution.y, mapSize.x, mapSize.y) {}

        /// <summary>
        /// Should be called when resolution is changed.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetViewportSize(int width, int height)
        {
            _viewportWidth = width;
            _viewportHeight = height;
            
            clampPosition();
        }

        public void SetZoom(float zoom)
        {
            _zoom = zoom;
            
            clampPosition();
        }

        public void MoveDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.Up:
                    _movementDirection.Y = -1;
                break;
                case Direction.Down:
                    _movementDirection.Y = 1;
                break;
                case Direction.Left:
                    _movementDirection.X = -1;
                break;
                case Direction.Right:
                    _movementDirection.X = 1;
                break;
            }
        }

        public Vector2 ScreenPositionToWorldPosition(Vector2 screenPosition)
        {
            var centerX = _viewportWidth / 2f;
            var centerY = _viewportHeight / 2f;

            var offsetX = screenPosition.X - centerX;
            var offsetY = screenPosition.Y - centerY;

            var worldX = _position.X + (offsetX / _zoom);
            var worldY = _position.Y + (offsetY / _zoom);

            return new Vector2(worldX, worldY);
        }

        public Vector2<int> WorldPositionToScreenPosition(float worldX, float worldY)
        {
            int centerX = _viewportWidth / 2;
            int centerY = _viewportHeight / 2;

            int screenX = centerX + (int)((worldX - _position.X) * _zoom);
            int screenY = centerY + (int)((worldY - _position.Y) * _zoom);

            return new(screenX, screenY);
        }

        public void Update(float? delta)
        {
            if(delta == null) return;

            if(_movementDirection.X != 0 || _movementDirection.Y != 0)
            {
                float moveDistance = MovementSpeed * (float)delta;

                move(_movementDirection.X * moveDistance, _movementDirection.Y * moveDistance);
            }

            _movementDirection = Vector2.Zero;
        }

        private void clampPosition()
        {
            var visibleWidth = _viewportWidth / _zoom;
            var visibleHeight = _viewportHeight / _zoom;

            var minX = visibleWidth / 2f;
            var maxX = _worldPixelWidth - (visibleWidth / 2f);

            var minY = visibleHeight / 2f;
            var maxY = _worldPixelHeight - (visibleHeight / 2f);

            if(minX > maxX)
                _position.X = _worldPixelWidth / 2f;
            else
                _position.X = Math.Clamp(_position.X, minX, maxX);

            if(minY > maxY)
                _position.Y = _worldPixelHeight / 2f;
            else
                _position.Y = Math.Clamp(_position.Y, minY, maxY);
        }

        private void move(float deltaX, float deltaY)
        {
            _position.X += deltaX;
            _position.Y += deltaY;

            clampPosition();
        }
    }
}