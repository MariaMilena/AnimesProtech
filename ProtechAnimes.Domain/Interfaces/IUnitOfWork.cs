﻿
namespace ProtechAnimes.Domain.Interfaces;

public interface IUnitOfWork
{
    IAnimeRepository AnimeRepository { get; }
    Task CommitAsync();
}
