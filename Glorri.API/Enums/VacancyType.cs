namespace Glorri.API.Enums
{
    public enum VacancyType
    {
        // İş saatına görə
        FullTime,        // Tam ştat
        PartTime,        // Yarım ştat
        Shift,           // Növbəli iş
        Flexible,        // Elastik iş saatı
        Remote,          // Uzaqdan iş
        Hybrid,          // Hibrid (ofis + remote)

        // Təcrübə / Seniorluq (S-E) üzrə
        Internship,      // Təcrübəçi
        Junior,          // Junior
        Middle,          // Middle
        Senior,          // Senior
        Lead,            // Team Lead / Expert

        // Müqavilə növünə görə (tez-tez əlavə olunur)
        Contract,        // Müqaviləli
        Freelance        // Freelance
    }
}
