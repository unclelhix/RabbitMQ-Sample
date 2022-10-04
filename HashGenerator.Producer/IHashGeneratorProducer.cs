namespace HashGenerator.Producer
{
    public interface IHashGeneratorProducer
    {
        /// <summary>
        /// Publish Message Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task Publish<T>(T data, string queue);
    }
}