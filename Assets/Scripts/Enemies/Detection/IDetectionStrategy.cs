using UnityEngine;

namespace Enemies.Detection
{
    public interface IDetectionStrategy {
        bool Execute(Transform player, Transform detector);
    }
}