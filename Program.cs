
using Lab1;
using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;

string projectRootFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
string filePath = $"{projectRootFolder}{Path.DirectorySeparatorChar}videogames.csv";
var videoGames = new List<VideoGame>();

//reading a list of video games 
using (var sr = new StreamReader(filePath))
{
    sr.ReadLine();

    while(!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        string[] lineData = line.Split(',');
        VideoGame vg = new VideoGame(lineData[0].Trim(), lineData[1].Trim(), lineData[2].Trim(), lineData[3].Trim(), lineData[4].Trim(), Convert.ToDouble(lineData[5].Trim()), 
                      Convert.ToDouble(lineData[6].Trim()), Convert.ToDouble(lineData[7].Trim()), Convert.ToDouble(lineData[8].Trim()), Convert.ToDouble(lineData[9].Trim()));
        videoGames.Add(vg);
    }

}

videoGames.Sort();


foreach(var item in videoGames)
{
    Console.WriteLine(item.ToString());
}


Console.WriteLine("Press enter to continue.");
Console.ReadLine();


//creating a list of nintendo games

var nintendoGames = from x in videoGames
                    where x.Publisher == "Nintendo"
                    select x;

//copied from code snippet on https://stackoverflow.com/questions/4003835/sort-list-in-c-sharp-with-linq

nintendoGames = nintendoGames.OrderBy(x => x.Name).ToList();

foreach(var item in nintendoGames)
{
    Console.WriteLine(item.ToString());
}

Console.WriteLine($"Out of {videoGames.Count()} games, {nintendoGames.Count()} are from Nintendo. " +
    $"This means {String.Format("{0:0%}", Convert.ToString(Math.Round(Convert.ToDouble(nintendoGames.Count())/Convert.ToDouble(videoGames.Count()) * 100, 2)))}% of games are from Nintendo.");

Console.WriteLine("Press enter to continue.");
Console.ReadLine();

//selecting adventure genre games from the list of nintendo games

var sportGames = from x in videoGames
                             where x.Genre == "Sports"
                             select x;

sportGames = sportGames.OrderBy(x => x.Genre).ToList();

foreach(var item in sportGames)
{
    Console.WriteLine(item.ToString());
}

Console.WriteLine($"Out of {videoGames.Count()} games, {sportGames.Count()} are sport games. This means " +
    $"{String.Format("{0:0%}", Convert.ToString(Math.Round(Convert.ToDouble(sportGames.Count()) / Convert.ToDouble(videoGames.Count()) * 100, 2)))}% of games have the sport genre.");

Console.WriteLine("Press enter to continue.");
Console.ReadLine();

//user-input publisher data retrieval
Console.Write("Enter a publisher you'd like to filter by: ");
string publisher = Console.ReadLine();
Console.WriteLine(PublisherData(videoGames, publisher));

//user-input genre data retrieval
Console.Write("Enter a genre you'd like to filter by: ");
string genre = Console.ReadLine();
Console.WriteLine(GenreData(videoGames, genre));



//PublisherData Section
string PublisherData(List<VideoGame> videoGames, string publisher)
{
    string msg = "";
    var selectedGames = from x in videoGames
                        where x.Publisher == publisher
                        select x;
    selectedGames = selectedGames.OrderBy(x => x.Publisher).ToList();
    if(selectedGames.Count() == 0)
    {
        msg += "No publishers found by that name.";
        return msg;
    }
    foreach(var item in selectedGames)
    {
        msg += item.ToString();
        msg += "\n";
    }
    
    msg += $"Out of {videoGames.Count()} games, {selectedGames.Count()} are {publisher} games. This means " +
    $"{String.Format("{0:0%}", Convert.ToString(Math.Round(Convert.ToDouble(selectedGames.Count()) / Convert.ToDouble(videoGames.Count()) * 100, 2)))}% of games are from {publisher}.";
       
    return msg;
}

string GenreData(List<VideoGame> videoGames, string genre)
{
    string msg = "";
    var selectedGames = from x in videoGames
                        where x.Genre == genre
                        select x;
    selectedGames = selectedGames.OrderBy(x => x.Genre).ToList();
    if (selectedGames.Count() == 0)
    {
        msg += "No games found by that genre.";
        return msg;
    }
    foreach (var item in selectedGames)
    {
        msg += item.ToString();
        msg += "\n";
    }
    
    msg += $"Out of {videoGames.Count()} games, {selectedGames.Count()} are {genre} games. This means " +
    $"{String.Format("{0:0%}", Convert.ToString(Math.Round(Convert.ToDouble(selectedGames.Count()) / Convert.ToDouble(videoGames.Count()) * 100, 2)))}% of games are from {genre}.";

    return msg;
}



