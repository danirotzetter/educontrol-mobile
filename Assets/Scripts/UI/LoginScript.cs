﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using aictr.data;

namespace aictr.UI
{
    public class LoginScript : Singleton<LoginScript>
    {

        [SerializeField]
        private Text username;
        [SerializeField]
        private Text password;
        [SerializeField]
        private Image iconLoggedIn;
        [SerializeField]
        private Image iconLoggedOut;
                [SerializeField]
        private Animator loginSlider;

        /// <summary>
        /// Stores credentials
        /// </summary>
        public void SaveLogin()
        {

            DataControl.Instance.DataBuffer.Username = username.text;
            DataControl.Instance.DataBuffer.Password = password.text;
            RefreshLoginStatus();

            // Hide the login form
            ToggleLogin();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        void Awake()
        {              

            // Login slider is hidden by default
            loginSlider.SetBool("isHidden", true);

            RefreshLoginStatus();
        }


        /// <summary>
        /// Restore credentials if already set in the DataControl
        /// </summary>
        private void RefreshLoginStatus()
        {
            // If available, restore the already preset variables
            if (!string.IsNullOrEmpty(DataControl.Instance.DataBuffer.Username))
                username.text = DataControl.Instance.DataBuffer.Username;
            if (!string.IsNullOrEmpty(DataControl.Instance.DataBuffer.Password))
                password.text = DataControl.Instance.DataBuffer.Password;

            bool isLoggedIn = DataControl.Instance.DataBuffer.IsLoggedIn();
            // Update the login status in the header
            iconLoggedIn.enabled = isLoggedIn;
            iconLoggedOut.enabled = !isLoggedIn;
            // Update the login status in the menu  
            MenuScript.Instance.SetLoginMenuItem(!isLoggedIn);
        }
               
        public void ToggleLogin() { ToggleLogin(null); }

        /// <summary>
        /// Open / close login screen
        /// <param name="forceShow">Optional parameter to force a visibility state as opposed to just toggle</param>
        /// </summary>
        public void ToggleLogin(bool? forceShow)
        {
            bool newStatus = false;
            if (forceShow != null)
            {
            newStatus= !(bool)forceShow;
            }
            else
            {
                // Just toggle
            bool isHidden = loginSlider.GetBool("isHidden");
                newStatus = !isHidden;
            }
            loginSlider.SetBool("isHidden", newStatus);
        }
    }
}