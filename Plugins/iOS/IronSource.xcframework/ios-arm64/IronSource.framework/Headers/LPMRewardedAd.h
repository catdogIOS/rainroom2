//
//  LPMRewardedAd.h
//  IronSource
//
//  Copyright © 2024 IronSource. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "LPMRewardedAdDelegate.h"

@class LPMRewardedAdConfig;

NS_ASSUME_NONNULL_BEGIN

/**
 Class responsible for handling the APIs, callbacks and overall operations of a rewarded ad.
 */
@interface LPMRewardedAd : NSObject

/**
 * A unique identifier associated with the ad object.
 */
@property(nonatomic, strong, readonly) NSString *adId;

- (instancetype)init NS_UNAVAILABLE;
- (instancetype)new NS_UNAVAILABLE;

/**
 Initializes a rewarded ad.

 @param adUnitId The ad unit identifier.
 */
- (instancetype)initWithAdUnitId:(NSString *)adUnitId;

/**
 Initializes a rewarded ad.

 @param adUnitId The ad unit identifier.
 @param config The ad configuration.
 */
- (instancetype)initWithAdUnitId:(NSString *)adUnitId config:(LPMRewardedAdConfig *)config;

/**
 Sets a delegate for the ad callbacks.
 The callbacks will be invoked on the main thread.
 The delegate is held weakly.

 @param delegate The delegate to set.
 */
- (void)setDelegate:(id<LPMRewardedAdDelegate>)delegate;

/**
 Loads a rewarded ad.
 The delegate will send a `didLoadAdWithAdInfo:` or
 `didFailToLoadAdWithAdUnitId: error:` callback.
 The callbacks will be invoked on the main thread.
 */
- (void)loadAd NS_SWIFT_NAME(loadAd());

/**
 Shows the ad.
 The delegate will send a `rewardedAdDidShow:` or
 `rewardedAd:didFailToShowWithError:` callback.

 @param viewController The view controller where the ad will be shown.
 @param placementName The placement name for the ad.
 */
- (void)showAdWithViewController:(UIViewController *)viewController
                   placementName:(nullable NSString *)placementName
    NS_SWIFT_NAME(showAd(viewController:placementName:));

/**
 Checks if the ad is ready.
 Showing an ad that is not ready will result in a show failure.
 When calling to the show api with placement, make sure to verify that placement isn't capped via
 `isPlacementCapped:`.

 @return Whether the ad is ready.
 */
- (BOOL)isAdReady;

/**
 Checks if the placement is capped.

 @param placementName The placement name to check.
 @return `YES` if the placement is capped, `NO` otherwise.
 */
+ (BOOL)isPlacementCapped:(NSString *)placementName;

@end

NS_ASSUME_NONNULL_END
