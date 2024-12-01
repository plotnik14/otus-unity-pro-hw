using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem.Data;
using SaveSystem.GameRepositories;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class ResourcesSaveLoader : SaveLoader<IEnumerable<ResourceData>, ResourceService>
    {
        public ResourcesSaveLoader(IGameStateRepository repository, ResourceService service) : base(repository, service) { }

        protected override IEnumerable<ResourceData> ConvertToData(ResourceService service)
        {
            List<ResourceData> resourcesData = new();

            foreach (Resource resource in service.GetResources())
            {
                var resourceData = new ResourceData
                {
                    ID = resource.ID,
                    Amount = resource.Amount,
                };
                resourcesData.Add(resourceData);
            }

            return resourcesData;
        }

        protected override void SetupData(IEnumerable<ResourceData> data, ResourceService service)
        {
            foreach (Resource resource in service.GetResources())
            {
               RestoreResourceStateByData(resource, data);
            }
        }

        protected override void SetupDefaultData(ResourceService service)
        {
            service.SetResources(new HashSet<Resource>());
        }

        /// <summary>
        /// Восстановление количества ресурсов, для существующих на сцене ресурсов.
        /// </summary>
        private static void RestoreResourceStateByData(Resource resource, IEnumerable<ResourceData> data)
        {
            ResourceData resourceData = data.FirstOrDefault(rd => rd.ID == resource.ID);

            if (resourceData == null)
            {
                Debug.LogError($"Failed to restore state of resource with id:{resource.ID}");
                return;
            }

            resource.Amount = resourceData.Amount;
        }
    }
}