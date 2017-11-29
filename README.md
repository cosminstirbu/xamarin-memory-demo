# Xamarin Memory Demo
### iOS Memory Leaks
- Using __Xcode Instruments__ run the application with __Allocations__
- Use `ViewController` in the `Instrument Detail` search view in __Xcode Instruments__ - notice there is only `ItemsViewController` and `AboutViewController`
- Tap on `AddItem` and you'll see a new view controller in __Xcode Instruments__ - `ItemNewViewController`
- Tap `Back` and you'll see that `ItemNewViewController` is not deallocated in __Xcode Instruments__
- Comment the `ViewDidLoad` method in `ItemNewViewController` and uncomment the `ViewWillAppear` / `ViewWillDissapear` methods
- Do the steps above once again an you'll see that `ItemNewViewController` is deallocated after tapping `Back`