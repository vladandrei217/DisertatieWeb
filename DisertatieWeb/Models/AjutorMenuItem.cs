namespace DisertatieWeb.Models
{
    public class AjutorMenuItem
    {
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public List<AjutorMenuItem> SubMenu { get; set; }

        public AjutorMenuItem(string text, string path, string icon)
        {
            Text = text;
            Path = path;
            Icon = icon;
            SubMenu = new List<AjutorMenuItem>();
        }

        // Metodă pentru a adăuga un submeniu
        public void AddSubMenu(AjutorMenuItem subMenu)
        {
            SubMenu.Add(subMenu);
        }
    }

}
