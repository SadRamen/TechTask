Bootstrap Image Loader:
      Functions:
          - Enter URL through 'TMP_InputField'
          - Asynchronized loading of images with URL and percentages
          - Asynchronized loading of images from 'Resources' folder and percentages
          - Error feedback
          - UI switch
      Technical Details:
          - Using Unity API 
              - 'UnityWebRequestTexture.GetTexture' for URL image load
              - 'Resources.LoadAsync<Sprite>' for local resources load
          - UniTask is used for asynchronization
          - UI components
              - Image
              - Slider
              - TMP_InputField
              - TMP_Text
              
Projectile.cs refactoring:
      - Interface 'IHaveProjectileReaction' was created with 'Reaction(Collision collision)' method which 
      - Refactored 'OnCollisionEnter' in Projectile.cs
      - Removed duplicates of 'Destroy(gameObject)'
      - Removed tag comparison


List of edited and refactored scripts:
        -Projectile.cs
List of created scripts:
        -AsyncSceneLoader
        -Bootstrap
        -IHaveProjectileReaction
        -ExplosiveBarrelLogic
        -GasTankLogic
        -TargetLogic
