namespace DisertatieWeb.Models
{
    public class AjutorMenuService
    {
        public AjutorMenuItem GetAjutorMenu()
        {
            // Creare submeniuri
            var documentatieItem = new AjutorMenuItem("Documentație", "documentatie", "menu_book");
            var contactItem = new AjutorMenuItem("Contact", "contact", "contact_support");

            // Creare meniu principal Ajutor
            var ajutorMenu = new AjutorMenuItem("Ajutor", "", "help_outline");

            // Adăugare submeniuri
            ajutorMenu.AddSubMenu(documentatieItem);
            ajutorMenu.AddSubMenu(contactItem);

            return ajutorMenu;
        }
    }

}
