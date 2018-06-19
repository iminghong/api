using NetCore.Api.IService;
using NetCore.IRepository.Entities;
using NetCore.Repository.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NetCore.Api.Service
{
    public class ActivityService: IActivityService
    {
        private readonly IActivityRepository _iActivityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository iActivityRepository, IUnitOfWork unitOfWork)
        {
            _iActivityRepository = iActivityRepository;
            _unitOfWork = unitOfWork;
        }

        public string Get()
        {
            var model = _iActivityRepository.GetAll();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Error,
                Error = delegate (object obj, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    args.ErrorContext.Handled = false;
                }
            };
            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeStyles = DateTimeStyles.RoundtripKind,
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });
            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }
    }
}
