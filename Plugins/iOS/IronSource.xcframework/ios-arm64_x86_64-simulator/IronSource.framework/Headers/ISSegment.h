//
//  ISSegment.h
//  IronSource
//
//  Created by Gili Ariel on 06/07/2017.
//  Copyright © 2017 Supersonic. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ISGender.h"

DEPRECATED_MSG_ATTRIBUTE("Use LPMSegment instead.")
@interface ISSegment : NSObject

@property(nonatomic) int age DEPRECATED_MSG_ATTRIBUTE("");
@property(nonatomic) int level;
@property(nonatomic) double iapTotal;
@property(nonatomic) BOOL paying;
@property(nonatomic) ISGender gender DEPRECATED_MSG_ATTRIBUTE("");
@property(nonatomic, strong) NSDate *userCreationDate;
@property(nonatomic, strong) NSString *segmentName;
@property(nonatomic, strong, readonly) NSDictionary *customKeys;

- (void)setCustomValue:(NSString *)value forKey:(NSString *)key;

- (NSDictionary *)getData;

@end
