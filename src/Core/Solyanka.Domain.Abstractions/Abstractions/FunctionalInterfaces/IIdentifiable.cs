namespace Solyanka.Domain.Abstractions.Abstractions.FunctionalInterfaces
{
    /// <summary>
    /// Interface representating the ability to uniquely identify a unit
    /// </summary>
    /// <typeparam name="TIdentificator">Type of identificator</typeparam>
    public interface IIdentifiable<out TIdentificator>
    {
        /// <summary>
        /// Unit identificator
        /// </summary>
        TIdentificator Id { get; }
    }
}