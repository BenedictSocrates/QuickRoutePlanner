using EveQuickRoutePlanner.Core.Models;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // TODO replace with map generated from static data
        var map = new UniverseMap(
            solarSystems:
            [
                new SolarSystem(1, "Jita", 0.9, 10000002),
                new SolarSystem(2, "Perimeter", 0.9, 10000002),
                new SolarSystem(3, "New Caldari", 0.9, 10000002),
                new SolarSystem(4, "Niyabainen", 0.9, 10000002),
            ],
            stations:
            [
                new Station(100, "Jita IV - Moon 4 - Caldari Navy Assembly Plant", 1),
                new Station(101, "Jita 4 - Moon 4 - Caldari Business Tribunal Bureau Offices", 1),
                new Station(102, "Jita 2 - Sebiestor Tribe Trade Embassy", 1),
                new Station(103, "New Caldari 4 - Moon 1 - Chief Executive Panel Bureau", 3),
                new Station(104, "New Caldari 5 - Moon 2 - House of Records Archives", 3),
                new Station(105, "New Caldari II (Matias) - Kaalakiota Corporation Factory", 3),
                new Station(106, "Niyabainen 4 - Moon 1 - Caldari Navy Assembly Plant", 4),
                new Station(107, "Niyabainen 8 - Moon 6 - Expert Housing Production Plant", 4),
                new Station(109, "Perimeter 2 - Moon 1 - Caldari Navy Assembly Plant", 2),
                new Station(110, "Perimeter 2 - Moon 1 - Ducia Foundry Mineral Reserve", 2),
            ],
            starGates:
            [
                (1,2),
                (1,3),
                (1,4),
                (3,4),
                (4,2),
            ]);

        builder.Services.AddSingleton(map);

        builder.Services.AddCors(o => o.AddPolicy("dev", policyBuilder =>
            policyBuilder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

        var app = builder.Build();
        app.UseCors("dev");

        app.MapGet("/api/health", (UniverseMap map) => Results.Ok(new { status = "ok", solarSystems = map.SolarSystemCount, stations = map.StationCount }));

        app.Run();
    }
}