﻿using System.Diagnostics;
using NitroxClient.Unity.Helper;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NitroxClient.MonoBehaviours.Gui.MainMenu
{
    public class MainMenuMods : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            if (scene.name == "XMenu")
            {
                DiscordMenuMods();
                MultiplayerMenuMods();                
            }
        }


        private void DiscordMenuMods()
        {
            GameObject startButton = GameObjectHelper.RequireGameObject("Menu canvas/Panel/MainMenu/PrimaryOptions/MenuButtons/ButtonPlay");
            GameObject showLoadedMultiplayer = Instantiate(startButton, startButton.transform.parent);
            showLoadedMultiplayer.name = "ButtonMultiplayer";
            Text buttonText = showLoadedMultiplayer.RequireGameObject("Circle/Bar/Text").GetComponent<Text>();
            buttonText.text = "Наш дискорд";
            Destroy(buttonText.GetComponent<TranslationLiveUpdate>());
            showLoadedMultiplayer.transform.SetSiblingIndex(3);
            Button showLoadedMultiplayerButton = showLoadedMultiplayer.GetComponent<Button>();
            showLoadedMultiplayerButton.onClick.RemoveAllListeners();
            showLoadedMultiplayerButton.onClick.AddListener(ShowDiscord);
        }

        private void MultiplayerMenuMods()
        {
            GameObject startButton = GameObjectHelper.RequireGameObject("Menu canvas/Panel/MainMenu/PrimaryOptions/MenuButtons/ButtonPlay");
            GameObject showLoadedMultiplayer = Instantiate(startButton, startButton.transform.parent);
            showLoadedMultiplayer.name = "ButtonMultiplayer";
            Text buttonText = showLoadedMultiplayer.RequireGameObject("Circle/Bar/Text").GetComponent<Text>();
            buttonText.text = "Игровые сервера";
            Destroy(buttonText.GetComponent<TranslationLiveUpdate>());
            showLoadedMultiplayer.transform.SetSiblingIndex(3);
            Button showLoadedMultiplayerButton = showLoadedMultiplayer.GetComponent<Button>();
            showLoadedMultiplayerButton.onClick.RemoveAllListeners();
            showLoadedMultiplayerButton.onClick.AddListener(ShowMultiplayerMenu);

            MainMenuRightSide rightSide = MainMenuRightSide.main;
            GameObject savedGamesRef = FindObject(rightSide.gameObject, "SavedGames");
            GameObject LoadedMultiplayer = Instantiate(savedGamesRef, rightSide.transform);
            LoadedMultiplayer.name = "Multiplayer";
            LoadedMultiplayer.RequireTransform("Header").GetComponent<Text>().text = "Мульти-пульти плеер by !Frez & Karchetta";
            Destroy(LoadedMultiplayer.RequireGameObject("Scroll View/Viewport/SavedGameAreaContent/NewGame"));
            Destroy(LoadedMultiplayer.GetComponent<MainMenuLoadPanel>());

            MainMenuMultiplayerPanel panel = LoadedMultiplayer.AddComponent<MainMenuMultiplayerPanel>();
            panel.SavedGamesRef = savedGamesRef;
            panel.LoadedMultiplayerRef = LoadedMultiplayer;

            rightSide.groups.Add(LoadedMultiplayer);
        }

        private void ShowMultiplayerMenu()
        {
            MainMenuRightSide rightSide = MainMenuRightSide.main;
            rightSide.OpenGroup("Multiplayer");
        }

        private void ShowDiscord()
        {
            MainMenuRightSide rightSide = MainMenuRightSide.main;
            rightSide.HideRightSide();
            try
            {
                Process.Start("https://discordapp.com/invite/hhNBmR6");
            }
            catch { }
        }

        private GameObject FindObject(GameObject parent, string name)
        {
            Component[] trs = parent.GetComponentsInChildren(typeof(Transform), true);
            foreach (Component t in trs)
            {
                if (t.name == name)
                {
                    return t.gameObject;
                }
            }

            return null;
        }
    }
}
