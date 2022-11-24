namespace Caty.Tools.Share.Repository
{
    /// <summary>
    /// 通用数据库主键
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        T Id { get; set; }
    }
}
