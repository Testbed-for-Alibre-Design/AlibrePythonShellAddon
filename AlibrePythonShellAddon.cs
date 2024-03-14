using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using AlibreAddOn;
using AlibreX;
using AlibrePythonShell;
using System.Windows;
using System.Threading;
using System.Windows.Threading;


namespace AlibrePythonShellAddon
{
    public class AlibrePythonShellAddon : IAlibreAddOn
    {
        private const int MenuIdRoot = 401;
        private const int MenuIdUtils = 601;
        private int[] _menuIdsRoot;
        private IADRoot _alibreRoot;
        private readonly IntPtr _parentWinHandle;
        public AlibrePythonShellAddon(IADRoot alibreRoot, IntPtr parentWinHandle)
        {
            _alibreRoot = alibreRoot;
            _parentWinHandle = parentWinHandle;
   
            //PythonShell.Topmost = true;
            BuildMenuTree();
        }
        public int RootMenuItem => MenuIdRoot;
        private void BuildMenuTree()
        {
            _menuIdsRoot = new int[1]
            {
               MenuIdUtils
            };
        }
        public bool HasSubMenus(int menuId)
        {
            switch (menuId)
            {
                case MenuIdRoot: return true;
            }
            return false;
        }
        public Array SubMenuItems(int menuId)
        {
            switch (menuId)
            {
                case MenuIdRoot: return _menuIdsRoot;
            }
            return null;
        }
        public string MenuItemText(int menuId)
        {
            switch (menuId)
            {
                case MenuIdRoot: return "Global IronPython Console";
            }
            return "Global IronPython Console";
        }
        public bool PopupMenu(int menuId)
        {
            return false;
        }
        public ADDONMenuStates MenuItemState(int menuId, string sessionIdentifier)
        {
            var session = _alibreRoot.Sessions.Item(sessionIdentifier);
            switch (session)
            {
                case IADDrawingSession:
                    switch (menuId)
                    {
                       case MenuIdRoot: return ADDONMenuStates.ADDON_MENU_ENABLED;
                    }
                    break;
                case IADAssemblySession:
                    switch (menuId)
                    {
                        case MenuIdRoot: return ADDONMenuStates.ADDON_MENU_ENABLED;
                    }
                    break;
                case IADPartSession:
                    switch (menuId)
                    {
                       case MenuIdRoot: return ADDONMenuStates.ADDON_MENU_ENABLED;
                    }
                    break;
            }
            return ADDONMenuStates.ADDON_MENU_ENABLED;
        }
        public string MenuItemToolTip(int menuId)
        {
            switch (menuId)
            {
                case MenuIdUtils: return "Global IronPython Console";
            }
            return "";
        }
        public string MenuIcon(int menuId)
        {
             switch (menuId)
             {
               case MenuIdUtils: return "logo.ico";
             }
            return "Global IronPython Console";
        }
        public bool HasPersistentDataToSave(string sessionIdentifier)
        {
            return false;
        }
        public IAlibreAddOnCommand InvokeCommand(int menuId, string sessionIdentifier)
        {
            var session = _alibreRoot.Sessions.Item(sessionIdentifier);
            switch (menuId)
            {
                case MenuIdUtils:
                {
                    return LauncherAddonCmd(session);
                }
            }
            return null;
        }
        private void OpenNewWindowOnSeparateThread()
        {

        }
        private IAlibreAddOnCommand LauncherAddonCmd(IADSession currentSession)
        {
            
            System.Windows.MessageBox.Show("AlibrePythonShell Addon", "Global IronPython ConsoleCommand");
            
            //Window cliW = new IronPython.

            Window PythonShell = new MainWindow(currentSession);
            PythonShell.ShowDialog();
            //PythonShell.ShowDialog();
            //PythonShell.Topmost = true;
            // Create a new thread;
            
            return null;
        }
        public void SaveData(IStream pCustomData, string sessionIdentifier)
        {
        }
        public void setIsAddOnLicensed(bool isLicensed)
        {
        }
        public bool UseDedicatedRibbonTab()
        {
            return true;
        }
        public void LoadData(IStream pCustomData, string sessionIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}