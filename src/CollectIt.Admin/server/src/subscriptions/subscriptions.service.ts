import {Injectable} from '@nestjs/common';
import {InjectModel} from "@nestjs/sequelize";
import {Subscription} from "./subscriptions.model";
import {Restriction} from "./restrictions/restriction.model";
import {RestrictionsService} from "./restrictions/restrictions.service";
import {ResourceType} from "../common/resource-type";
import {RestrictionType} from "./restrictions/restriction-type";
import CreationException from "../common/creation.exception";

@Injectable()
export class SubscriptionsService {
    constructor(@InjectModel(Subscription) private subscriptionsRepository: typeof Subscription,
                private restrictionService: RestrictionsService) {  }

    async createSubscriptionAsync(name: string,
                                  description: string,
                                  price: number,
                                  monthDuration: number,
                                  appliedResourceType: ResourceType,
                                  maxResourcesCount: number,
                                  active: boolean | null,
                                  restrictionType: RestrictionType | null = null,
                                  authorId: number | null = null,
                                  sizeBytes: number | null = null,
                                  daysTo: number | null = null,
                                  daysAfter: number | null = null,
                                  tags: string[] | null = null): Promise<Subscription> {
        let restriction: Restriction | null = null;
        let subscription: Subscription | null = null;
        try {
            if (restrictionType) {
                switch (restrictionType) {
                    case RestrictionType.Author:
                        restriction = await this.restrictionService.createAuthorRestrictionAsync(authorId);
                        break;
                    case RestrictionType.DaysTo:
                        restriction = await this.restrictionService.createDaysToRestrictionAsync(daysTo);
                        break;
                    case RestrictionType.DaysAfter:
                        restriction = await this.restrictionService.createDaysAfterRestrictionAsync(daysAfter);
                        break;
                    case RestrictionType.Tags:
                        restriction = await this.restrictionService.createTagsRestrictionAsync(tags);
                        break;
                    case RestrictionType.Size:
                        restriction = await this.restrictionService.createSizeRestrictionAsync(sizeBytes);
                        break;
                    default:
                        throw new CreationException('Could not create restriction',[`Unsupported restriction type: ${restrictionType}`]);
                }
            }
            const errors: string[] = []
            if (monthDuration < 1) {
                errors.push('Month duration must be positive');
            }
            if (name?.length < 6) {
                errors.push('Minimum name length is 6');
            }
            if(description?.length < 10) {
                errors.push('Minimum description length is 10');
            }
            if(price < 0) {
                errors.push('Price can not be negative');
            }
            if(maxResourcesCount < 1) {
                errors.push('Max resources count must be positive');
            }

            if (errors.length > 0) {
                throw new CreationException('Could not create subscription', errors)
            }

            subscription = await this.subscriptionsRepository.create({
                name: name,
                description: description,
                maxResourcesCount: maxResourcesCount,
                price: price,
                monthDuration: monthDuration,
                appliedResourceType: appliedResourceType,
                restrictionId: restriction?.id,
                active: active ?? false,
            });
            return subscription;
        } catch (e) {
            if (restriction) {
                await this.restrictionService.deleteRestrictionById(restriction.id);
            }
            if (subscription) {
                await this.subscriptionsRepository.destroy({
                    where: {
                        id: subscription.id
                    }
                });
            }
            throw e;
        }
    }

    async getSubscriptionById(subscriptionId: number): Promise<Subscription> {
        const subscription = await this.subscriptionsRepository.findByPk(subscriptionId);
        return subscription;
    }

    async getSubscriptionsByResourceType(resourceType: ResourceType,
                                         pageNumber: number | null,
                                         pageSize: number | null) {
        return await this.subscriptionsRepository.findAndCountAll({
            where: {
                appliedResourceType: resourceType
            },
            limit: pageSize,
            offset: pageNumber
                    ? (pageNumber - 1) * pageSize
                    : null
        });
    }
}
