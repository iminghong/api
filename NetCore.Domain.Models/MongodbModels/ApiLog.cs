using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Domain.Models.MongodbModels
{
    public class ApiLog : IMongodbEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public string ResultValue { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string LogType { get; set; }

        /// <summary>
        /// 插入时间
        /// </summary>
        public string InputTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public string ExecTime { get; set; }
    }
}
