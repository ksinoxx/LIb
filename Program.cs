using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Library> repo =
    [
        new ("Алексей Гоннчаров", "Успех или успеть", "об упехе",1),
        new ("Лев Толстой","Анна Каренина", "роман современной жизни",2)
    ];



app.MapGet("/", () => repo);
app.MapPost("/", (Library lib) => repo.Add(lib));
app.MapPut("/{num}", (LibraryUpDTO dto, int num) =>
{
    Library buffer = repo.Find(lib => lib.Num == num);
    if (buffer == null)
        return Results.StatusCode(404);
    if(buffer.Autor != dto.autor)
        buffer.Autor = dto.autor;
    if (buffer.Title != dto.title)
        buffer.Title = dto.title;
    if (buffer.Discription != dto.discription)
        buffer.Discription = dto.discription;
    return Results.Json(buffer);
});
app.MapDelete("/{num}", (int num) =>
{
    Library buffer = repo.Find(lib => lib.Num == num);
    repo.Remove(buffer);
});
app.Run();

record class LibraryUpDTO(string autor, string title, string discription);
class Library
{
    int num;
    string autor;
    string title;
    string discription;

    public Library(string autor, string title, string discription, int num)
    {
        Autor = autor;
        Title = title;
        Discription = discription;
        Num = num;
    }

    public string Autor { get => autor; set => autor = value; }
    public string Title { get => title; set => title = value; }
    public string Discription { get => discription; set => discription = value; }
    public int Num { get => num; set => num = value; }
}
