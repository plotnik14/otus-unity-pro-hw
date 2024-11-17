using SaveSystem.GameRepositories;

namespace SaveSystem.SaveLoaders
{
    public abstract class SaveLoader<TData, TService> : ISaveLoader
    {
        private readonly IGameRepository _repository;
        private readonly TService _service;

        protected SaveLoader(IGameRepository repository, TService service)
        {
            _repository = repository;
            _service = service;
        }

        public void SaveGame()
        {
            TData data = ConvertToData(_service);
            _repository.SetData(data);
        }

        public void LoadGame()
        {
            if (_repository.TryGetData(out TData data))
            {
                SetupData(data, _service);
                return;
            }

            SetupDefaultData(_service);
        }

        protected abstract TData ConvertToData(TService service);

        protected abstract void SetupData(TData data, TService service);

        protected virtual void SetupDefaultData(TService service) { }
    }
}