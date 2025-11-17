using Godot;
using System;
using System.Threading.Tasks;

public partial class TestControl : Control
{
    [Export]
    public Button PlayMusicButton;

    [Export]
    public Button PauseMusicButton;

    [Export]
    public Button PlaySoundEffectButton;

    [Export]
    public Button NextPageButton;

    public override void _Ready()
    {
        base._Ready();

        AudioStream soundEffect = ResourceLoader.Load<AudioStream>($"{Paths.SoundEffects}/world_sfx.ogg");

        AudioStream music = ResourceLoader.Load<AudioStream>($"{Paths.Music}/default_music.mp3");

        PackedScene nextPage = ResourceLoader.Load<PackedScene>("uid://by5y1ekxx86xy");

        PlaySoundEffectButton.Pressed += async () =>
        {
            await AudioManager.Instance.PlaySoundEffect(soundEffect: soundEffect);
        };

        PlayMusicButton.Pressed += () =>
        {
            AudioManager.Instance.StartMusicTrack(audioStream: music);
        };

        PauseMusicButton.Pressed += () =>
        {
            AudioManager.Instance.PauseMusic();
        };

        NextPageButton.Pressed += () =>
        {
            Node node = nextPage.Instantiate();
            GD.Print(node);
            ScaffoldManager.Instance.ScaffoldNewSceneTree(newSceneTree: node);
        };
    }
}
