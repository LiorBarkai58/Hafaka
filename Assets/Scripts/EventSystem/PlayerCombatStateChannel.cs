using Player;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(menuName = "Events/PlayerCombatStateChannel")]
    
    public class PlayerCombatStateChannel : EventChannel<PlayerCombatState>
    {
        
    }
}