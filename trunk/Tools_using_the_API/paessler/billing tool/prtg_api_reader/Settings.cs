﻿/*
 * Copyright (c) 2011, Paessler AG
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 * 
 *     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
 *       the documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Paessler AG nor the names of its contributors may be used to endorse or promote products derived from this
 *       software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
namespace Paessler.Billingtool
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class Settings
    {
        #region Fields

        public string CompanyLogo;
        public string DefaultBillingFooter;
        public string DefaultBillingHeader;
        public string FooterText;
        public string OutputFolder;
        public string ScriptFolder;
        public int SelectedGroup;
        public int SelectedServer;
        public List<TreeProbeElement> SelectedServerSensoorTree;
        public ServerList ServerSettings;
        public string TemplateFolder;

        private static readonly Settings instance = new Settings();

        #endregion Fields

        #region Constructors

        private Settings()
        {
            if (Properties.prtg_server.Default.ServerList != null)
            {
                ServerSettings = Properties.prtg_server.Default.ServerList;
                SelectedServer = Properties.prtg_server.Default.SelectedServer;
                SelectedGroup = Properties.prtg_server.Default.SelectedGroup;
            }
            else
            {
                ServerSettings = new ServerList();
                SelectedServer = -1;
                SelectedGroup = -1;
            }

            ScriptFolder = Properties.prtg_server.Default.ScriptFolder;
            TemplateFolder = Properties.prtg_server.Default.TemplateFolder;

            if (string.IsNullOrEmpty(Properties.prtg_server.Default.DefaultOutPath) 
                || string.IsNullOrWhiteSpace(Properties.prtg_server.Default.DefaultOutPath))
            {
                OutputFolder = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\";
            }
            else
            {
                OutputFolder = Properties.prtg_server.Default.DefaultOutPath;
            }

            DefaultBillingHeader = Properties.prtg_server.Default.DefaultBillingHeader;
            DefaultBillingFooter = Properties.prtg_server.Default.DefaultBillingFooter;

            FooterText = Properties.prtg_server.Default.FooterText;
            CompanyLogo = Properties.prtg_server.Default.CompanyLogo;
        }

        #endregion Constructors

        #region Properties

        public static Settings Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion Properties

        #region Methods

        public ServerSettings GetSelectedServerSettings(int serverId)
        {
            return ServerSettings.Server[SelectedServer];
        }

        public void SaveSettings()
        {
            Properties.prtg_server.Default.ServerList = ServerSettings;
            Properties.prtg_server.Default.SelectedServer = SelectedServer;
            Properties.prtg_server.Default.SelectedGroup = SelectedGroup;
            Properties.prtg_server.Default.ScriptFolder = ScriptFolder;
            Properties.prtg_server.Default.TemplateFolder = TemplateFolder;
            Properties.prtg_server.Default.DefaultOutPath = OutputFolder;
            Properties.prtg_server.Default.DefaultBillingHeader = DefaultBillingHeader;
            Properties.prtg_server.Default.DefaultBillingFooter = DefaultBillingFooter;
            Properties.prtg_server.Default.CompanyLogo = CompanyLogo;
            Properties.prtg_server.Default.FooterText = FooterText;
            Properties.prtg_server.Default.Save();
        }

        #endregion Methods
    }
}