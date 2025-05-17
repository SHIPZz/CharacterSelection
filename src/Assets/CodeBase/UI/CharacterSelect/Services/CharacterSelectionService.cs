using System.Collections.Generic;
using System.Linq;
using CodeBase.Common.Services.Persistent;
using CodeBase.Data;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;
using Zenject;

namespace CodeBase.UI.CharacterSelect.Services
{
    public class CharacterSelectionService : ICharacterSelectionService, IInitializable, IProgressWatcher
    {
        private readonly List<CharacterData> _characters = new();
        private readonly ReactiveProperty<CharacterData> _currentCharacter = new();
        private readonly CharacterConfig _characterConfig;

        private int _currentCharacterIndex;

        public IReadOnlyList<CharacterData> Characters => _characters;

        public IReadOnlyReactiveProperty<CharacterData> CurrentCharacter => _currentCharacter;

        public CharacterSelectionService(CharacterConfig characterConfig) => _characterConfig = characterConfig;

        public void Initialize() => _characters.AddRange(_characterConfig.Characters);

        public void SwitchToNextCharacter()
        {
            if (_characters.Count == 0)
                return;

            _currentCharacterIndex = (_currentCharacterIndex + 1) % _characters.Count;
            _currentCharacter.Value = _characters[_currentCharacterIndex];
        }

        public void SwitchToPreviousCharacter()
        {
            if (_characters.Count == 0)
                return;

            _currentCharacterIndex = (_currentCharacterIndex - 1 + _characters.Count) % _characters.Count;
            _currentCharacter.Value = _characters[_currentCharacterIndex];
        }

        public void SetCharacter(CharacterTypeId characterId)
        {
            CharacterData targetCharacter = _characters.Find(x => x.TypeId == characterId);

            _currentCharacterIndex = _characters.IndexOf(targetCharacter);
            _currentCharacter.Value = targetCharacter;
        }

        public void Save(ProgressData progressData)
        {
            progressData.PlayerData.LastSelectedCharacter = _currentCharacter.Value;
            progressData.PlayerData.LastSelectedCharacterIndex = _currentCharacterIndex;
        }

        public void Load(ProgressData progressData)
        {
            CharacterData lastSavedCharacter = progressData.PlayerData.LastSelectedCharacter;

            CharacterData characterData =
                _characterConfig.Characters.FirstOrDefault(x => x.TypeId == lastSavedCharacter.TypeId);
            lastSavedCharacter.SetIcons(characterData.Icon, characterData.Background, characterData.MainBackground);

            _currentCharacter.Value = lastSavedCharacter;
            _currentCharacterIndex = progressData.PlayerData.LastSelectedCharacterIndex;
        }
    }
}