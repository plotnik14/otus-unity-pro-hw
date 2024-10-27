namespace ShootEmUp
{
    /// <summary>
    /// Marker interface to ensure a class bound in the DI will be instantiated even if there are no active
    /// references to it, as lazy loading is the default behavior.
    /// </summary>
    public interface INonLazy { }
}