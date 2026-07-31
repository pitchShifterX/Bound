
using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Gameplay;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.Player;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Unit;

namespace GameEngine.World.Input
{
    public class SelectionService
    {
        private PlayerService _player;
        private ECSService _ecs;
        private ICameraView _camera;

        private const float _singleSelectionRadiusSquared = 16f * 16f;

        public bool IsDragging { get; private set; }

        public Vector2<int> DragStartPosition { get; private set; }
        public Vector2<int> DragCurrentPosition { get; private set; }

        public SelectionService(PlayerService player, ECSService ecs, ICameraView camera)
        {
            _player = player;
            _ecs = ecs;
            _camera = camera;
        }

        public Rectangle<int> SelectionRectangle
        {
            get
            {
                var left = Math.Min(DragStartPosition.x, DragCurrentPosition.x);
                var top = Math.Min(DragStartPosition.y, DragCurrentPosition.y);

                return new Rectangle<int>(
                    left,
                    top,
                    Math.Abs(DragCurrentPosition.x - DragStartPosition.x),
                    Math.Abs(DragCurrentPosition.y - DragStartPosition.y)
                );
            }
        }

        public void Start(Vector2<int> mousePosition)
        {
            DragStartPosition = mousePosition;
            DragCurrentPosition = mousePosition;

            IsDragging = false;
        }

        public void Update(Vector2<int> mousePosition)
        {
            DragCurrentPosition = mousePosition;

            if(Math.Abs(DragCurrentPosition.x - DragStartPosition.x) > 4 ||
                Math.Abs(DragCurrentPosition.y - DragStartPosition.y) > 4)
            {
                IsDragging = true;
            }
        }

        public void End()
        {
            IsDragging = false;
        }

        public void SelectUnits()
        {
            clearSelection();

            if (!IsDragging)
            {
                selectSingleUnit(DragCurrentPosition);

                return;
            }

            selectUnitsInRectangle();
        }

        private void selectSingleUnit(Vector2<int> screenPosition)
        {
            var worldPosition = _camera.ScreenPositionToWorldPosition(
                screenPosition.x,
                screenPosition.y
            );

            int? closestUnit = null;
            float closestDistance = float.MaxValue;
            bool closestIsCurrentPlayer = false;

            var entities = _ecs.GetEntitiesWith<UnitComponent, TransformComponent, PlayerOwnerComponent>();

            foreach(var entity in entities)
            {
                ref var player = ref _ecs.GetComponent<PlayerOwnerComponent>(entity);

                bool isCurrentPlayer = player.PlayerOwnerId == _player.CurrentPlayer.Id;

                ref var transform = ref _ecs.GetComponent<TransformComponent>(entity);

                float distance = Vector2<float>.DistanceSquared(
                    transform.Position,
                    worldPosition
                );

                if(distance <= _singleSelectionRadiusSquared &&
                   distance < closestDistance)
                {
                    closestUnit = entity;
                    closestDistance = distance;
                    closestIsCurrentPlayer = isCurrentPlayer;
                }

            }

            if(closestUnit.HasValue)
            {
                addSelection(closestUnit.Value, closestIsCurrentPlayer);
            }
        }

        private void selectUnitsInRectangle()
        {
            var entities = _ecs.GetEntitiesWith<UnitComponent, TransformComponent, PlayerOwnerComponent>();

            foreach(var entity in entities)
            {
                bool isCurrentPlayer = false;

                ref var player =
                    ref _ecs.GetComponent<PlayerOwnerComponent>(entity);

                if(player.PlayerOwnerId == _player.CurrentPlayer.Id)
                    isCurrentPlayer = true;

                ref var transform =
                    ref _ecs.GetComponent<TransformComponent>(entity);

                var screenPosition = _camera.WorldPositionToScreenPosition(
                    transform.Position.x,
                    transform.Position.y
                );

                if(SelectionRectangle.Contains(screenPosition))
                {
                    addSelection(entity, isCurrentPlayer);
                }
            }
        }

        private void addSelection(int entity, bool currentPlayer)
        {
            ref var settings = ref _ecs.GetComponent<SelectionCircleSettingsComponent>(entity);

            _ecs.AddComponent(
                entity,
                new SelectedUnitByPlayerComponent
                {
                    PlayerId = _player.CurrentPlayer.Id
                }
            );

            _ecs.AddComponent(
                entity,
                new SelectionCircleComponent
                {
                    Radius = settings.Radius,
                    Offset = settings.Offset,
                    Color = currentPlayer ? Color.Green : Color.Red
                }
            );
        }

        private void clearSelection()
        {
            var entities = _ecs.GetEntitiesWith<SelectedUnitByPlayerComponent>().ToList();

            foreach(var entity in entities)
            {
                _ecs.RemoveComponent<SelectedUnitByPlayerComponent>(entity);
                _ecs.RemoveComponent<SelectionCircleComponent>(entity);
            }
        }
    }
}