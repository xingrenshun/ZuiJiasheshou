const unityNamespace = {
      unityVersion: '2022.3.48f1c1', 
      assetBundleFSEnabled: true,
      assetBundleBufferCapacity: 128,
      assetBundleBufferTTL: 5,
      // DATA_CDN + dataFileSubPrefix + datafilename
      dataFileSubPrefix:'',
      optimizeWebGLMemoryInBackground: true,
      bootConfig: 'player-connection-ip=192.168.1.8',
	  UnityModule: null,
}
GameGlobal.unityNamespace = GameGlobal.unityNamespace || unityNamespace;
module.exports = unityNamespace;