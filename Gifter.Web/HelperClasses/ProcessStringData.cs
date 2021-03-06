﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gifter.DataAccess;

namespace Gifter.Web.HelperClasses
{
    public class ProcessStringData
    {
        public static List<int> getTagsFromString(String s)
        {
            String[] tags = s.Split(',');
            List<int> tagIds = new List<int>();
            foreach (String s_tag in tags)
            {
                String tmp = s_tag.Trim();
                using (var context = new GifterDBEntities())
                {
                    Tags tag = context.Tags.FirstOrDefault(x => x.TagName.Equals(tmp));
                    if (tag == null)
                    {
                        tag = new Tags();
                        tag.TagName = tmp;
                        context.Tags.Add(tag);
                        context.SaveChanges();
                    }
                    tagIds.Add(tag.TagId);
                }
            }
            return tagIds;
        }

        public static List<int> getPresentsFromString(String s)
        {
            String[] presents = s.Split(',');
            List<int> presentIds = new List<int>();
            foreach (String s_present in presents)
            {
                String tmp = s_present.Trim();
                using (var context = new GifterDBEntities())
                {
                    Gifts gift = context.Gifts.FirstOrDefault(x => x.GiftName.Equals(tmp));
                    if (gift == null)
                    {
                        gift = new Gifts();
                        gift.GiftName = tmp;
                        context.Gifts.Add(gift);
                        context.SaveChanges();
                    }
                    presentIds.Add(gift.GiftId);
                }
            }
            return presentIds;
        }

        public static void createUser(String userName, List<int> tags, List<int> gifts)
        {
            using (var context = new GifterDBEntities())
            {
                Users user = new Users();
                user.UserName = userName;
                foreach (int i in tags)
                {
                    user.Tags.Add(context.Tags.First(x => x.TagId == i));
                }
                foreach (int i in gifts)
                {
                    user.Gifts.Add(context.Gifts.First(x => x.GiftId == i));
                }
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void createFakeUser(List<int> tags, List<int> gifts)
        {
            createUser("unknown", tags, gifts);
        }

        public static void createUser(String userName, String tags, String gifts)
        {
            createUser(userName, getTagsFromString(tags), getPresentsFromString(gifts));
        }

        public static void createFakeUser(String tags, String gifts)
        {
            createUser("unknown", tags, gifts);
        }

        public static Dictionary<String, double> getAnswer(List<int> tags)
        {
            Dictionary<String, double> giftsMap = new Dictionary<String, double>(); // mapping gifts to percentages
            using (var context = new GifterDBEntities())
            {
                List<Users> users = context.Users.ToList();
                foreach (Users user in users)
                {
                    int c11 = 0;
                    foreach (Tags tag in user.Tags)
                    {
                        if (tags.Contains(tag.TagId))
                            c11++;
                    }
                    if (c11 == 0)
                    {
                        continue;
                    }
                    int c10 = tags.Count - c11;
                    int c01 = user.Tags.Count - c11;
                    double sim = 1.0 * c11 / (c11 + 2.0 * (c10 + c01));
                    foreach (Gifts gift in user.Gifts)
                    {
                        double value;
                        if (giftsMap.TryGetValue(gift.GiftName, out value))
                        {
                            giftsMap[gift.GiftName] = value + sim;
                        }
                        else
                        {
                            giftsMap.Add(gift.GiftName, sim);
                        }
                    }
                }
            }
            if (giftsMap.Count == 0)
            {
                giftsMap.Add("Million dollars :D", 1);
            }
            giftsMap = giftsMap.OrderBy(x => -x.Value).ToDictionary(x => x.Key, x => x.Value);
            double maxVal = giftsMap.Select(x => x.Value).Max();
            var result = new Dictionary<String, double>();
            foreach (var x in giftsMap)
            {
                result.Add(x.Key, x.Value / maxVal);
            }
            return result;
        }

        public static Dictionary<String, double> getAnswer(String tags)
        {
            return getAnswer(getTagsFromString(tags));
        }

        public static String getTags()
        {
            String res = "";
            using (var context = new GifterDBEntities())
            {
                foreach (Tags tag in context.Tags)
                {
                    if (res.Length > 0)
                        res += ",";
                    res += tag.TagName;
                }
            }
            return res;
        }

        public static String getGifts()
        {
            String res = "";
            using (var context = new GifterDBEntities())
            {
                foreach (Gifts gift in context.Gifts)
                {
                    if (res.Length > 0)
                        res += ",";
                    res += gift.GiftName;
                }
            }
            return res;
        }
    }
}