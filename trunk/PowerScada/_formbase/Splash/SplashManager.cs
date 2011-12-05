using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using System.Drawing;

//=========================================================================================================
// Module:       SplashManager
// Date:         05/01/2006
// Revised:      29/10/2006
// Author:       Tyron Harford
//=========================================================================================================

namespace PowerScada
{
    namespace Forms
    {
        public partial class SplashManager
        {


            #region " Class Definition "
            private Forms.frmSplash _SplashForm;
            //
            private String _ApplicationName;
            private String _ApplicationText;
            private String _Licencee;
            private String _Version;
            private Image _ApplicationImage;
            private String _Copyright;
            #endregion

            #region " Properties "
            ///===============================================================================================
            /// <summary>
            /// Gets or sets the Application name.
            /// </summary>
            ///===============================================================================================
            public String ApplicationName
            {
                get
                {
                    return this._ApplicationName;
                }
                set
                {
                    this._ApplicationName = value;
                    if (this._SplashForm != null)
                    {
                        this._SplashForm.lblApplicationName.Text = value;
                    }
                }
            }
            ///===============================================================================================
            /// <summary>
            /// Gets or sets the Application text.
            /// </summary>
            ///===============================================================================================
            public String ApplicationText
            {
                get
                {
                    return this._ApplicationText;
                }
                set
                {
                    this._ApplicationName = value;
                    if (this._SplashForm != null)
                    {
                        this._SplashForm.lblApplicationText.Text = value;
                    }
                }
            }
            ///===============================================================================================
            /// <summary>
            /// Gets or sets the version, will get prefixed with "licenced to: ".
            /// </summary>
            ///===============================================================================================
            public String Licencee
            {
                get
                {
                    return this._Licencee;
                }
                set
                {
                    this._ApplicationName = value;
                    if (this._SplashForm != null)
                    {
                        if (value != "")
                        {
                            this._SplashForm.lblLicencee.Text = "licenced to: " + value;
                        }
                        else
                        {
                            this._SplashForm.lblLicencee.Text = "";
                        }
                    }
                }
            }
            ///===============================================================================================
            /// <summary>
            /// Gets or sets the version, will get prefixed with "version ".
            /// </summary>
            ///===============================================================================================
            public String Version
            {
                get
                {
                    return this._Version;
                }
                set
                {
                    this._Version = value;
                    if (this._SplashForm != null)
                    {
                        if (value != "")
                        {
                            _SplashForm.lblVersion.Text = "version " + value;
                        }
                        else
                        {
                            _SplashForm.lblVersion.Text = "";
                        }
                    }
                }
            }
            ///===============================================================================================
            /// <summary>
            /// Gets or sets the Application image (will get resized to 519x198).
            /// </summary>
            ///===============================================================================================
            public Image ApplicationImage
            {
                get
                {
                    return this._ApplicationImage;
                }
                set
                {
                    this._ApplicationImage = value;
                    if (this._SplashForm != null)
                    {
                        try
                        {
                            if (value != null)
                            {
                                _SplashForm.pbApplicationImage.Image = value;
                            }
                            else
                            {
                                _SplashForm.pbApplicationImage.Image = null;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            ///===============================================================================================
            /// <summary>
            /// Gets or sets the Copyright text.
            /// </summary>
            ///===============================================================================================
            public String Copyright
            {
                get
                {
                    return this._Copyright;
                }
                set
                {
                    this._ApplicationName = value;
                    if (this._SplashForm != null)
                    {
                        this._SplashForm.lblCopyright.Text = value;
                    }
                }
            }
            #endregion

            #region " Methods "
            ///===============================================================================================
            /// <summary>
            /// Shows the splash screen.
            /// </summary>
            /// <remarks></remarks>
            ///===============================================================================================
            public void ShowSplashScreen()
            {
                this._SplashForm.Show();
            }
            ///===============================================================================================
            /// <summary>
            /// Hides the splash screen.
            /// </summary>
            /// <remarks></remarks>
            ///===============================================================================================
            public void HideSplashScreen()
            {
                this._SplashForm.Hide();
            }
            ///===============================================================================================
            /// <summary>
            /// Closes the splash screen.
            /// </summary>
            /// <remarks></remarks>
            ///===============================================================================================
            public void CloseSplashScreen()
            {
                this._SplashForm.Close();
               
            }
            ///===============================================================================================
            /// <summary>
            /// Refresh the splash screen. Use when changing properties.
            /// </summary>
            /// <remarks></remarks>
            ///===============================================================================================
            public void Refresh()
            {
                this._SplashForm.Refresh();
            }
            #endregion

            #region " Constructors "
            ///===============================================================================================
            /// <summary>
            /// Creates a new instance of the SplashManager.
            /// </summary>
            /// <remarks></remarks>
            ///===============================================================================================
            public SplashManager()
            {
                this._SplashForm = new Forms.frmSplash();
            }
            ///===============================================================================================
            /// <summary>
            /// Creates a new instance of the SplashManager.
            /// </summary>
            /// <param name="ApplicationName">The Application name.</param>
            /// <param name="ApplicationText">The text that describes the operation of the Application.</param>
            /// <param name="Licencee">The licencee of the Application.</param>
            /// <param name="Version">The version of the Application.</param>
            /// <param name="ApplicationImage">The Application image.</param>
            /// <param name="Copyright">The copyright text of the application.</param>
            /// <remarks></remarks>
            ///===============================================================================================
            public SplashManager(String ApplicationName, String ApplicationText, String Licencee, String Version, Image ApplicationImage, String Copyright)
            {
                //
                this._SplashForm = new Forms.frmSplash();
                //
                this.ApplicationName = ApplicationName;
                this.ApplicationText = ApplicationName;
                this.Licencee = ApplicationName;
                this.Version = ApplicationName;
                this.ApplicationImage = ApplicationImage;
                this.Copyright = Copyright;
            }
            #endregion

            public void Opacity()
            {
                this._SplashForm.Opacity = 0;
                System.Windows.Forms.Application.DoEvents();
            }    
        
        }

       
    }
}
