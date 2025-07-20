using System.Reflection;

namespace Boards.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(BoardsController).Assembly;
}
