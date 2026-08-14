using GameEngine;
using GameEngine.Utilities;
using GameEngine.World.Map.Triggers;
using Mods.Bound.UI.Events;

namespace Mods.Bound.Gameplay.Triggers.Actions
{
    public class SetScoreAction : ITriggerAction
    {
        private int _playerId;
        private int _score;
        private string _method;

        public SetScoreAction(int playerId, int value, string method)
        {
            _playerId = playerId;
            _score = value;
            _method = method;
        }

        public TriggerActionResult Execute(IGameplayContext context, float? delta)
        {
            int currentScore = 0;

            if(context.Player.TryGetCustomVariableValueForPlayer(
                _playerId,
                "score",
                out var value
            ))
            {
                if(value is not int score)
                    return TriggerActionResult.Completed;

                currentScore = score;
            }

            if(_method == "divide" && _score == 0)
                return TriggerActionResult.Completed;

            int newScore = _method switch
            {
                "add"      => currentScore + _score,
                "subtract" => currentScore - _score,
                "multiply" => currentScore * _score,
                "divide"   => currentScore / _score,
                _          => currentScore
            };

            context.Player.SetCustomVariableForPlayer(
                _playerId,
                "score",
                newScore
            );

            context.UIEvents.Publish(new ScoreEvent(_playerId, newScore));

            Log.Info($"Score is now {newScore}");

            return TriggerActionResult.Completed;
        }
    }
}