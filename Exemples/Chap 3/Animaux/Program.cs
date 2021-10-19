// See https://aka.ms/new-console-template for more information
using Animaux;

Animal chef = new Animal();
Desaffecter (chef);
System.Console.WriteLine(chef is null);


void Desaffecter(Animal a)
{
    a = null; 
}
