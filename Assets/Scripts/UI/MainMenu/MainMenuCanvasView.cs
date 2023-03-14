using System;
using System.Globalization;
using UI.Common;
using UnityEngine;

namespace UI.MainMenu
{
    public sealed class MainMenuCanvasView : MonoBehaviour
    {
        [field: SerializeField] public ButtonView StartGameButton { get; private set; }
        [field: SerializeField] public ButtonView ResetRecordButton { get; private set; }
        [field: SerializeField] public ButtonView ExitGameButton { get; private set; }
        [field: SerializeField] public TextView RecordNumber { get; private set; }

        public void Init(Action startGameButton, Action resetRecordButton, Action exitGameButton, float recordNumber)
        {
            StartGameButton.Init(startGameButton);
            ResetRecordButton.Init(resetRecordButton);
            ExitGameButton.Init(exitGameButton);

            RecordNumber.Init(recordNumber.ToString(CultureInfo.InvariantCulture));
        }
    }
}