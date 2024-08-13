namespace Core.Api
{
    public interface dfghsjsdf {}

    public interface IGameListener : dfghsjsdf {}

    public interface IUpdateListener : IGameListener
    {
        void OnUpdate();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }
}