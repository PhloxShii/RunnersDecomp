# Why this fork exists.

I get that you need the specific versions of the SDKs and what not to build this, but not everyone has money to go and buy them. Some people just wanna be able to use the decompiled game to make levels or whatever.

Like yeah piracy is bad and you SHOULD buy the sdk and ngui and what not. But some people just don't have the money to do so.

So yeah, this fork comes with the specific versions of the SDKs and what not already installed, so now you can play the game in-editor without any bs.

Phlox out.

# Sonic Runners (2015) Decompilation

This is a largely faithful decompilation of the final release of Sonic Runners (version 2.0.3). It was initially sourced from the Android version, but will build to any platform that Unity supports.

Some components have been removed for quality-of-life purposes, such as Facebook integration, Google Play Games integration, and Noah (Sega ad service) integration. Additionally, some libraries cannot be included in this repository for legal reasons. You must acquire them yourself - see the "Getting Started" section for more details.

## Getting Started

### Setting up the project

Sonic Runners depends on **CriWare Unity SDK v1.20.3.0** and **NGUI v3.0.2** in order to function correctly. Other versions may work but it isn't guaranteed. Both of these packages are available on the Unity Asset Store.

It's recommended to place these in the project directory **before** opening the project for the first time:

- Place your CriWare plugin into `Assets\Plugins`.
- Place your NGUI plugin into `Assets\NGUI`.

By doing this, you will prevent scenes from losing component references related to these plugins.

### Setting up Unity

For this project, it's highly recommended to use Unity 4.6.9. That was the version of Unity that was last used by Sega for this project, and later versions are known to cause compatibility issues.

Unity versions before 5.x have been "soft-delisted" from the website, however there are still direct links available here: https://forum.unity.com/threads/early-unity-versions-downloads.1483758/

By default, Unity will be in personal/free mode, but unfortunately some parts of Runners (notably CriWare and AssetBundles) require "pro" mode. Legally, we cannot provide this, you'll need to acquire a Unity Pro license key yourself for Runners to function correctly.

### AssetBundles

Sonic Runners relied on Unity AssetBundles to deliver gameplay components (e.g. stages, characters, etc.) from the server to the client. These have been decompiled into a [separate repository](https://github.com/itsmattkc/RunnersAssetBundleDecomp) that can optionally be installed inside this repository. The client can be modified and compiled completely independently from the AssetBundles, and can even be run in Unity to an extent, however the AssetBundles available from the production server have only been built for Android and iOS, and will not function correctly on Windows/Mac/Linux.

Sonic Runners has a development mode that allows loading the assets locally rather than from compiled server AssetBundles. To enable this:

1. Clone the [asset bundles repository](https://github.com/itsmattkc/RunnersAssetBundleDecomp) into a folder called `AssetBundles` in the project's Assets folder. If the folder is cloned as `RunnersAssetBundleDecomp`, it must be renamed to `AssetBundles`.
1. In Unity, open the project's build settings (File > Build Settings) and add all of the scenes from the AssetBundle repository.
1. Open `Env.cs` and set the variable `m_useAssetBundle` to `false`. This will tell the game to load scenes locally rather than attempt to download them.

When building the client, the AssetBundle scenes should be unchecked and `m_useAssetBundle` should be set back to `true` so it operates with servers again. While it is theoretically possible to build a completely local copy of Sonic Runners as-is by leaving this mode on and keeping all of the scenes included, the build is likely to be very large, and will obviously not be able to receive any new content without rebuilding and redistributing the entire project.

## Building

While in many cases building Runners is straightforward, at least for low-maintenance platforms like Windows/Mac/Linux, for Android and iOS, some extra work is required due to how old Unity 4.6.9 is, and how rapidly smartphone APIs have evolved since then (especially iOS).

### Building for Android
1. In order to target Android, Unity will need the Android SDK installed, but the latest version of the Android SDK is incompatible with Unity 4.x. You'll need to install Android 6.0 (Marshmallow) SDK, which is the last version that will work correctly.
1. In Unity's settings, ensure the Android SDK path is pointing specifically to 6.0.
1. Android SDK 6.0 is technically still _slightly_ too new for Unity 4.x, namely its "tools" folder, which we'll need to downgrade. [Download an older version of the tools here](http://dl-ssl.google.com/android/repository/tools_r25.2.5-windows.zip).
1. Replace the `tools` folder in your Android 6.0 SDK with the one you downloaded above.
1. Done! Building for Android should now work as expected.

### Building for iOS
1. For iOS building, Unity produces an Xcode project rather than a finished executable. This project must be compiled with Xcode which is only available on macOS, so ensure you have access to a Mac before building this.
1. Go to the Player Settings for iOS in Unity.
1. Ensure that the Graphics API is set to Metal.
1. Build the Xcode project through Unity (This could take upwards of an hour).
1. While you can build with the latest Xcode, you will need some assets from Xcode 9 so install both for now.
1. Add libstdc++ libraries from Xcode 9 into your Xcode installation. After this, you can uninstall Xcode 9.
1. Open the Xcode project with Xcode.
1. Set the project to use the Legacy Build System in "Project Settings".
1. In "Build Settings", add `-fdeclspec` as a C build flag.
1. In "Info", under "Custom iOS Target Settings", add `App Transport Security Settings`, and then add `Allow Arbitrary Loads` and set it to YES.
1. Also in "Info", add `Application supports iTunes file sharing` and set it to YES.
1. In "General", under "Frameworks, Libraries, and Embedded Content", add `VideoToolbox.framework`, `Metal.framework`, `MetalKit.framework`, and `MetalPerformanceShaders.framework`.
1. If you get a permissions error upon building, ensure that you give permission to `MapFileParser.sh` to execute by running `chmod +x MapFileParser.sh`
1. Compile the Xcode project and deploy to your iOS device.

## Contributing

This repository is intended to be as faithful of a decompilation of the original game as is reasonably possible. Therefore we will not be accepting features/requests/bug fixes that are not present in the original game. If you notice a decompilation inaccuracy or any other issue that prevents using or building this repository as-is, feel free to let us know, or submit a pull request to fix it.

## Credits

- Decompilation by [MattKC](https://github.com/itsmattkc/) with help from [Ramen2X](https://github.com/ramen2x)
- Original Sonic Runners server reimplementation by [fluofoxxo](https://github.com/fluofoxxo/outrun)
- Open source preparations made by [\_F121_](https://github.com/F121Live)
