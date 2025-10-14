// See https://aka.ms/new-console-template for more information

using SurvivorSeeds;
using SurvivorSeeds.Interfaces;


ICreateMatches CreateMatchesSql = new CreateMatchesNoSql();

CreateMatchesSql.CrearPartidas();