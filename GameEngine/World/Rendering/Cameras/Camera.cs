using GameEngine.Utilities;

namespace GameEngine.World.Rendering.Cameras
{
    public class Camera : ICameraController, ICameraView
    {
        private Vector2<float> _position;
        private float _zoom = 2.0f;
        private float _movementSpeed = 300f;
        private Vector2<float> _movementDirection = Vector2<float>.Zero;

        private Rectangle<int> _viewport;

        private int _worldPixelWidth;
        private int _worldPixelHeight;

        public int ViewportWidth => _viewport.Width;
        public int ViewportHeight => _viewport.Height;

        public Rectangle<int> Viewport => _viewport;
        public Vector2<float> WorldPosition => _position;
        public float Zoom => _zoom;
        public float MovementSpeed => _movementSpeed;

        public Rectangle<float> VisibleWorldBounds
        {
            get
            {
                float width = ViewportWidth / Zoom;
                float height = ViewportHeight / Zoom;

                return new(
                    WorldPosition.x - width / 2,
                    WorldPosition.y - height / 2,
                    width,
                    height
                );
            }
        }

        public Camera(
            Rectangle<int> bounds,
            Vector2<int> mapSize,
            Vector2<float>? defaultPosition = null
        )
        {
            _viewport = bounds;
            _worldPixelWidth = mapSize.x * Constants.TileSize;
            _worldPixelHeight = mapSize.y * Constants.TileSize;

            _position = defaultPosition ?? new Vector2<float>(
                _worldPixelWidth / 2f, _worldPixelHeight / 2f
            );

            clampPosition();
        }

        public Camera(
            Rectangle<float> bounds,
            Vector2<int> mapSize,
            Vector2<float>? defaultPosition = null
        ) : this(bounds.To<int>(), mapSize, defaultPosition){}

        /// <summary>
        /// Should be called when resolution is changed.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetViewport(Rectangle<int> viewport)
        {
            _viewport = viewport;
            
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
                    _movementDirection.y = -1;
                break;
                case Direction.Down:
                    _movementDirection.y = 1;
                break;
                case Direction.Left:
                    _movementDirection.x = -1;
                break;
                case Direction.Right:
                    _movementDirection.x = 1;
                break;
            }
        }

        public bool IsVisible(Rectangle<float> bounds)
        {
            return VisibleWorldBounds.Intersects(bounds);
        }

        public Vector2<float> ScreenPositionToWorldPosition(int screenX, int screenY)
        {
            var vector2 = new Vector2<float>(screenX, screenY);

            return ScreenPositionToWorldPosition(vector2);
        }

        public Vector2<float> ScreenPositionToWorldPosition(Vector2<float> screenPosition)
        {
            var centerX = _viewport.X + _viewport.Width / 2f;
            var centerY = _viewport.Y + _viewport.Height / 2f;

            var offsetX = screenPosition.x - centerX;
            var offsetY = screenPosition.y - centerY;

            var worldX = _position.x + (offsetX / _zoom);
            var worldY = _position.y + (offsetY / _zoom);

            return new Vector2<float>(worldX, worldY);
        }

        public Vector2<int> WorldPositionToScreenPosition(float worldX, float worldY)
        {
            int centerX = _viewport.X + _viewport.Width / 2;
            int centerY = _viewport.Y + _viewport.Height / 2;

            int screenX = centerX + (int)((worldX - _position.x) * _zoom);
            int screenY = centerY + (int)((worldY - _position.y) * _zoom);

            return new(screenX, screenY);
        }

        public Rectangle<float> WorldToViewportRectangle(Rectangle<float> worldPosition)
        {
            var screenPosition = WorldPositionToScreenPosition(
                worldPosition.X,
                worldPosition.Y
            );

            return new Rectangle<float>
            {
                X = screenPosition.x,
                Y = screenPosition.y,
                Width = worldPosition.Width * Zoom,
                Height = worldPosition.Height * Zoom
            };
        }

        public void Update(float? delta)
        {
            if(delta == null) return;

            if(_movementDirection.x != 0 || _movementDirection.y != 0)
            {
                float moveDistance = MovementSpeed * (float)delta;

                move(_movementDirection.x * moveDistance, _movementDirection.y * moveDistance);
            }

            _movementDirection = Vector2<float>.Zero;
        }

        private void clampPosition()
        {
            var visibleWidth = _viewport.Width / _zoom;
            var visibleHeight = _viewport.Height / _zoom;

            var minX = visibleWidth / 2f;
            var maxX = _worldPixelWidth - (visibleWidth / 2f);

            var minY = visibleHeight / 2f;
            var maxY = _worldPixelHeight - (visibleHeight / 2f);

            if(minX > maxX)
                _position.x = _worldPixelWidth / 2f;
            else
                _position.x = Math.Clamp(_position.x, minX, maxX);

            if(minY > maxY)
                _position.y = _worldPixelHeight / 2f;
            else
                _position.y = Math.Clamp(_position.y, minY, maxY);
        }

        private void move(float deltaX, float deltaY)
        {
            _position.x += deltaX;
            _position.y += deltaY;

            clampPosition();
        }
    }
}