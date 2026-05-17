using CityExplorer.Models;

namespace CityExplorer.Data;

public static class DefaultPlaces
{
    public static IEnumerable<Place> All => new[]
    {
        // ─── 🏰 Ajalugu ────────────────────────────────────────────────────────
        new Place
        {
            Name            = "Toompea loss",
            Description     = "Eesti parlamendi asukoht – ehitatud 13. sajandil",
            FullDescription = "Toompea loss on Eesti riikluse süda. Ehitatud 13. sajandil taanlaste poolt, on see kindlus näinud sajandeid ajalugu. Täna asub siin Riigikogu – Eesti parlament. Ikooniline Pikk Hermann on Tallinna tuntuim vaatamisväärsus.",
            Address         = "Lossi plats 1a, Tallinn",
            CategoryEmoji   = "🏰",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/Toompea_loss_2014.jpg/960px-Toompea_loss_2014.jpg"
        },
        new Place
        {
            Name            = "Oleviste kirik",
            Description     = "Kuni 16. sajandini maailma kõrgeim hoone (159 m)",
            FullDescription = "Oleviste kirik on üks Tallinna tuntumaid vaatamisväärsusi. Kirik ehitati 12. sajandil ja kuni 1625. aastani oli see maailma kõrgeim hoone 159 meetriga. Torn on avatud külastajatele – sealt avaneb vapustav vaade Tallinna vanalinnale.",
            Address         = "Lai 50, Tallinn",
            CategoryEmoji   = "🏰",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/St_Olaf%27s_church%2C_Tallinn%2C_July_2008.jpg/960px-St_Olaf%27s_church%2C_Tallinn%2C_July_2008.jpg"
        },
        new Place
        {
            Name            = "Kiek in de Kök",
            Description     = "15. sajandi võimas suurtükitorn vanalinnas",
            FullDescription = "Kiek in de Kök on massiivselt tugevdatud keskaegne suurtükitorn, ehitatud umbes 1475. aastal. Nimi tähendab alamsaksa keeles \"piilu kööki\" – müürivahid said naaberelamute köökideni piiluda. Täna on torn muuseum.",
            Address         = "Komandandi tee 2, Tallinn",
            CategoryEmoji   = "🏰",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Kiek_in_de_K%C3%B6k%2C_Tallin%2C_Estonia%2C_2012-08-05%2C_DD_02.JPG/960px-Kiek_in_de_K%C3%B6k%2C_Tallin%2C_Estonia%2C_2012-08-05%2C_DD_02.JPG"
        },
        new Place
        {
            Name            = "Aleksander Nevski katedraal",
            Description     = "Vene õigeusu katedraal, ehitatud aastal 1900",
            FullDescription = "Aleksander Nevski katedraal on üks Tallinna tuntuimaid vaatamisväärsusi, ehitatud 1900. aastal Toompea kõrgeimale kohale. See on tsaarivõimu ja vene ortodoksuse sümbol Eestis.",
            Address         = "Lossi plats 10, Tallinn",
            CategoryEmoji   = "🏰",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Catedral_de_Alejandro_Nevsky%2C_Tallin%2C_Estonia%2C_2012-08-11%2C_DD_46.JPG/800px-Catedral_de_Alejandro_Nevsky%2C_Tallin%2C_Estonia%2C_2012-08-11%2C_DD_46.JPG"
        },

        // ─── 🌳 Pargid ─────────────────────────────────────────────────────────
        new Place
        {
            Name            = "Kadriorg park",
            Description     = "Peeter Suure loodud barokk-park ja KUMU kunstimuuseum",
            FullDescription = "Kadriorg on üks Tallinna armastatumaid parke, mille rajas Peeter Suur 1718. aastal. Pargis asub Kadrioru loss (nüüd kunstimuuseum), KUMU kunstimuuseum ja Eesti presidendi residents. Kevadel õitsevad kirsiõied.",
            Address         = "A. Weizenbergi 37, Tallinn",
            CategoryEmoji   = "🌳",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Tallinn_asv2022-04_img55_Kadriorg_Palace.jpg/1280px-Tallinn_asv2022-04_img55_Kadriorg_Palace.jpg"
        },
        new Place
        {
            Name            = "Stroomi rand",
            Description     = "Populaarne liivarand Tallinna lääneosas",
            FullDescription = "Stroomi rand on üks Tallinna lemmikutest suvituskohtadest. Pikk liivarand pakub võimalust ujuda, päevitada ja jalutada. Rand on hästi ligipääsetav ning ümbritsetud männimetsaga.",
            Address         = "Randla 11, Tallinn",
            CategoryEmoji   = "🌳",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/Stroomi_rand_2024.jpg/800px-Stroomi_rand_2024.jpg"
        },
        new Place
        {
            Name            = "Tondiraba park",
            Description     = "Tallinna suurim park spordi- ja puhkevõimalustega",
            FullDescription = "Tondiraba park on Tallinna suurim ja mitmekülgseim park. Siin asub Tondiraba jäähall, ujula, minigolfi väljak ja mänguväljakud lastele. Pargis on ilus tiik ja pikad jalutusrajad.",
            Address         = "Tondi 84, Tallinn",
            CategoryEmoji   = "🌳",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Tondiraba_Ice_Hall_014.JPG/800px-Tondiraba_Ice_Hall_014.JPG"
        },
        new Place
        {
            Name            = "Tallinna botaanikaaed",
            Description     = "Üle 8000 taimeliigiga botaanikaaed Pirital",
            FullDescription = "Tallinna botaanikaaed on asutatud 1961. aastal ja asub kaunil Pirita jõe kaldal. Aias kasvab üle 8000 taimeliigi, sealhulgas eksootilised troopikakasvuhooned, roosiaed ja niidud.",
            Address         = "Kloostrimetsa tee 52, Tallinn",
            CategoryEmoji   = "🌳",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Jard%C3%ADn_Bot%C3%A1nico_de_Tallinn%2C_Estonia%2C_2012-08-12%2C_DD_05.JPG/960px-Jard%C3%ADn_Bot%C3%A1nico_de_Tallinn%2C_Estonia%2C_2012-08-12%2C_DD_05.JPG"
        },

        // ─── 🍽️ Toidukohad ─────────────────────────────────────────────────────
        new Place
        {
            Name            = "Leib restoran",
            Description     = "Autentne eesti köök vanalinnas",
            FullDescription = "Leib restoran on pühendunud ehtsa eesti köögile. Menüüs on kohalikud hooajalised koostisosad ning traditsioonilised Eesti retseptid kaasaegse käsitlusega. Hubane atmosfäär vanalinnas.",
            Address         = "Uus 31, Tallinn",
            CategoryEmoji   = "🍽️",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/86/14-08-14-Tallinn-RalfR-110.jpg/960px-14-08-14-Tallinn-RalfR-110.jpg"
        },
        new Place
        {
            Name            = "Põhjala tehas",
            Description     = "Käsitööõlu ja toit Telliskivi loomelinnakus",
            FullDescription = "Põhjala tehas on Eesti üks armastatumaid käsitööõlle kohti, asudes Telliskivi loomelinnakus. Tehases saab maitsta värsket kohapealt pruulitud õlut ja nautida maitsvat toitu.",
            Address         = "Telliskivi 62, Tallinn",
            CategoryEmoji   = "🍽️",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/P%C3%B5hjala_Brewery_%26_Tap_Room_5_-_Renee_Altrov.jpg/960px-P%C3%B5hjala_Brewery_%26_Tap_Room_5_-_Renee_Altrov.jpg"
        },
        new Place
        {
            Name            = "Ribe restoran",
            Description     = "Peenköök keskaegses majas vanalinnas",
            FullDescription = "Ribe on üks Tallinna prestiižikamaid restorane, asudes ajaloolises keskaegses majas vanalinnas. Restoran pakub rahvusvahelist fine dining menüüd kohalike ja hooajaliste koostisosadega.",
            Address         = "Vana-Posti 1, Tallinn",
            CategoryEmoji   = "🍽️",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Telliskivi_Loomelinnak_2021_3.jpg/960px-Telliskivi_Loomelinnak_2021_3.jpg"
        },
        new Place
        {
            Name            = "F-hoone",
            Description     = "Trendikas kohvik-restoran Telliskivi loomelinnakus",
            FullDescription = "F-hoone on üks Tallinna populaarsemaid kohviku-restorane Telliskivi loomelinnakus. Mitmekesine menüü katab hommikusöögist kuni õhtusöögini, pakkudes nii liha- kui taimetoite.",
            Address         = "Telliskivi 60a, Tallinn",
            CategoryEmoji   = "🍽️",
            ImageUrl        = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Tallinn_Telliskivi_Loomelinnak_2021.a.jpg/960px-Tallinn_Telliskivi_Loomelinnak_2021.a.jpg"
        },
    };
}
