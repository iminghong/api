using Newtonsoft.Json;
using NetCore.Api.IService;
using NetCore.IRepository;
using NetCore.Repository.UnitOfWork;
using System;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace NetCore.Api.Service
{
    public class CFDAppInfoService : ICFDAppInfoService
    {
        private readonly ICFDAppInfoRepository _iCFDAppInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CFDAppInfoService(ICFDAppInfoRepository iCFDAppInfoRepository, IUnitOfWork unitOfWork)
        {
            _iCFDAppInfoRepository = iCFDAppInfoRepository;
            _unitOfWork = unitOfWork;
        }

        public virtual string Get()
        {
            var model = _iCFDAppInfoRepository.Get(p => p.Appid == 8);
            if (model != null)
            {
                model.updatetime = DateTime.Now;
            }
            _unitOfWork.Commit();
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
