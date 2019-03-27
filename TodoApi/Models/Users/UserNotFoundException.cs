namespace TodoApi.Models.Users
{
    using System;

    /// <summary>
    /// Исключение, которое возникает при попытке получить несуществующего пользователя
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Инициализировать экземпляр исключения по идентификатору пользователя
        /// </summary>
        /// <param name="userId"></param>
        public UserNotFoundException(Guid userId)
            : base($"A user by id \"{userId}\" is not found.")
        {
        }

        /// <summary>
        /// Инициализировать экземпляр исключения по логину пользователя
        /// </summary>
        /// <param name="login"></param>
        public UserNotFoundException(string login)
            : base($"A user by login \"{login}\" is not found.")
        {
        }
    }
}
