using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caty.Tools.Share.Repository
{
    /// <summary>
    /// 基础审计（共用）字段
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// 实体设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        /// <summary>
        /// 记录唯一编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
    }

    /// <summary>
    /// 实体拓展(带创建者和最后修改人)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public abstract class EntityExtend<T, K> : Entity<T>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建人唯一标识
        /// </summary>
        public virtual K CreatorId { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [MaxLength(50)]
        public virtual string Modifier { get; set; }

        /// <summary>
        /// 最后修改人唯一标识
        /// </summary>
        public virtual K ModifierId { get; set; }
    }
}
