using System.Collections.Generic;
using CodeBase.UI.CharacterSelect.Configs;
using UniRx;

namespace CodeBase.UI.CharacterSelect.Services
{
    public interface ICharacterSelectionService
    {
        IReadOnlyList<CharacterData> Characters { get; }
        IReadOnlyReactiveProperty<CharacterData> CurrentCharacter { get; }
        
        void SwitchToNextCharacter();
        void SwitchToPreviousCharacter();
    }
} 