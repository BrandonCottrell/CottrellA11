using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using Newtonsoft.Json;

namespace CottrellA4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string choice;

            do
            {
                Console.WriteLine("***** Movie Database *****");
                Console.WriteLine("1) View Movies");
                Console.WriteLine("2) Edit Movies");
                Console.WriteLine("Enter to quit");
                choice = Console.ReadLine();
                Console.WriteLine($"User Choice: {choice}");

                if (choice == "1")
                {
                    Console.WriteLine("*****Movie Database*****");
                    Console.WriteLine("Please make a selection");
                    Console.WriteLine("1) Search Movie");
                    Console.WriteLine("2) See all Movies");
                    Console.WriteLine("3) See # of Movies");

                    var firstResponse = Console.ReadLine();

                    if (firstResponse == "1")
                    {
                        Console.WriteLine("What movie are you looking for?");
                        var movieSearch = Console.ReadLine();
                        
                        using (var db = new MovieContext())
                        {
                            var movieToLookFor = db.Movies.Where(x => x.Title.Contains(movieSearch));
                            Console.WriteLine("*****Search Results*****");
                            foreach (var movie in movieToLookFor)
                            {
                                Console.WriteLine($"{movie.Id}. {movie.Title}");
                            }
                        }


                    }
                    else if (firstResponse == "2")
                    {
                        using (var db = new MovieContext())
                        {
                            var allMovies = db.Movies;
                            Console.WriteLine("*****All Movies*****");
                            foreach (var movie in allMovies)
                            {
                                Console.WriteLine($"{movie.Id}. {movie.Title}");
                            }
                        }
                    } else if (firstResponse == "3")
                    {
                        Console.WriteLine("How many movies would you like to see?");
                        int numOfMovies = Convert.ToInt32(Console.ReadLine());


                        using (var db = new MovieContext())
                        {
                            var allMovies = db.Movies;
                            Console.WriteLine($"*****{numOfMovies} Movies*****");
                            foreach (var movie in allMovies.Take(numOfMovies))
                            {
                                Console.WriteLine($"{movie.Id}. {movie.Title}");
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Not a valid response.");
                    }

                } else if (choice == "2")
                {
                    Console.WriteLine("*****Movie Database*****");
                    Console.WriteLine("1) Add Movie");
                    Console.WriteLine("2) Update Movie");
                    Console.WriteLine("3) Delete Movie");
                    var editResponse = Console.ReadLine();

                    if (editResponse == "1")
                    {
                        Console.WriteLine("*****Add Movie*****");
                        Console.WriteLine("Enter Movie Title");
                        var movieTitle = Console.ReadLine();

                        using (var db = new MovieContext())
                        {
                            var newMovie = new Movie();
                            newMovie.Title = movieTitle;

                            db.Movies.Add(newMovie);
                            db.SaveChanges();
                            Console.WriteLine("Movie Added.");
                        }

                    } else if (editResponse == "2")
                    {
                        Console.WriteLine("Enter Movie to update");
                        var updateMovie = Console.ReadLine();   

                        using (var db = new MovieContext())
                        {
                            var oldMovieTitle = db.Movies.FirstOrDefault(x => x.Title == updateMovie);

                            Console.WriteLine("Enter the updated Movie Name");
                            var newMovieTitle = Console.ReadLine();
                            oldMovieTitle.Title = newMovieTitle;
                           
                            db.SaveChanges();
                            Console.WriteLine($"Movie updated");

                        }


                    } else if (editResponse == "3")
                    {
                        Console.WriteLine("Which movie would you like to delete?");
                        var deleteInput = Console.ReadLine();

                        using (var db = new MovieContext())
                        {
                            var deleteTitle = db.Movies.FirstOrDefault(x => x.Title == deleteInput);
                            db.Movies.Remove(deleteTitle);
                            db.SaveChanges();

                            Console.WriteLine($"Movie Deleted");
                        }

                    } else
                    {
                        Console.WriteLine("Not a valid response.");
                    }

                }


            } while (choice == "1" || choice == "2");
        }
    }
}

