using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.CharacterSelect.Configs
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _characters = new();

        public IReadOnlyList<CharacterData> Characters => _characters;
    }
}