namespace MultiEarthquakeLib
{
    public class Earthquake
    {
        /// <summary>
        /// Merge Earthquake Early Warning 
        /// </summary>
        /// <param name="earthquake1">EarthquakeList</param>
        /// <param name="earthquake2">Another EarthquakeList</param>
        /// <returns>EarthquakeList. If either EarthquakeList contains null, it returns null.</returns>
        public EarthquakeList? MergeEarthquake(EarthquakeList? earthquake1,EarthquakeList? earthquake2)
        {

            //合成はearthquake1の情報をもとにして行われる。

            //nullが含まれていた時の処理
            if(earthquake1 == null || earthquake2 == null || earthquake1.items == null || earthquake2.items == null)
            {
                return null;
            }
            else
            {
                //返す時のやつを作る
                EarthquakeList earthquakeList = new();
                earthquakeList.items = new();
                earthquakeList.LastAction = "Merge";
                //earthquake2で追加済みのものの番号のリスト
                List<int> earthquake2_Added = new();


                //EventIdで検索(成長してforeach使ってるえらい)
                foreach (var item1 in earthquake1.items)
                {
                    bool find = false;//すでに存在しているかどうかのフラグ

                    //EventIdで捜索
                    int index = 0;
                    foreach(var item2 in earthquake2.items)
                    {
                        if(item1.EventId == item2.EventId)
                        {
                            //earthquake2_Addedに追加されたearthquake2のインデックス番号を追加
                            earthquake2_Added.Add(index);

                            //Serialを比較して情報を更新
                            //item2の報が新しいとき、そのまま追加 & どちらかが警報の場合はisWarinngをtrueに
                            if(item1.Serial < item2.Serial)
                            {
                                earthquakeList.items.Add(item2);//新しいほうを追加

                                //警報処理

                                //フラグ
                                if(item1.isWarning == true || item2.isWarning == true)
                                {
                                    //どちらかが警報の場合
                                    earthquakeList.items.Last().isWarning = true;
                                }
                                else
                                {
                                    //それ以外
                                    earthquakeList.items.Last().isWarning = false;
                                }

                                //新しい情報に警報地方情報が含まれていないかつ、古い情報に警報地域が含まれている場合
                                if(item2.WarningAreaDistrict == null && item1.WarningAreaDistrict != null)
                                {
                                    //古い情報の警報地域を追加
                                    earthquakeList.items.Last().WarningAreaDistrict = item1.WarningAreaDistrict;
                                }

                                //新しい情報に警報地域情報(地域単位)が含まれていないかつ、古い情報に警報地域が含まれている場合
                                if (item2.WarningAreaRegions == null && item1.WarningAreaRegions != null)
                                {
                                    //古い情報の警報地域を追加
                                    earthquakeList.items.Last().WarningAreaRegions = item1.WarningAreaRegions;
                                }



                            }

                            //item1の報が新しいとき、そのまま追加
                            else if (item1.Serial > item2.Serial)
                            {
                                earthquakeList.items.Add(item1);

                                //警報処理

                                //フラグ
                                if (item1.isWarning == true || item2.isWarning == true)
                                {
                                    //どちらかが警報の場合
                                    earthquakeList.items.Last().isWarning = true;
                                }
                                else
                                {
                                    //それ以外
                                    earthquakeList.items.Last().isWarning = false;
                                }

                                //新しい情報に警報地方情報が含まれていないかつ、古い情報に警報地域が含まれている場合
                                if (item1.WarningAreaDistrict == null && item2.WarningAreaDistrict != null)
                                {
                                    //古い情報の警報地域を追加
                                    earthquakeList.items.Last().WarningAreaDistrict = item2.WarningAreaDistrict;
                                }

                                //新しい情報に警報地域情報(地域単位)が含まれていないかつ、古い情報に警報地域が含まれている場合
                                if (item1.WarningAreaRegions == null && item2.WarningAreaRegions != null)
                                {
                                    //古い情報の警報地域を追加
                                    earthquakeList.items.Last().WarningAreaRegions = item2.WarningAreaRegions;
                                }
                            }

                            //item1,item2が同じとき
                            else if(item1.Serial == item2.Serial)
                            {
                                earthquakeList.items.Add(item2);
                                //警報処理

                                //フラグ
                                if (item1.isWarning == true || item2.isWarning == true)
                                {
                                    //どちらかが警報の場合
                                    earthquakeList.items.Last().isWarning = true;
                                }
                                else
                                {
                                    //それ以外
                                    earthquakeList.items.Last().isWarning = false;
                                }

                                //item2情報に警報地方情報が含まれていないかつ、古い情報に警報地域が含まれている場合
                                if (item2.WarningAreaDistrict == null && item1.WarningAreaDistrict != null)
                                {
                                    //古い情報の警報地域を追加
                                    earthquakeList.items.Last().WarningAreaDistrict = item1.WarningAreaDistrict;
                                }

                                //item2情報に警報地域情報(地域単位)が含まれていないかつ、古い情報に警報地域が含まれている場合
                                if (item2.WarningAreaRegions == null && item1.WarningAreaRegions != null)
                                {
                                    //古い情報の警報地域を追加
                                    earthquakeList.items.Last().WarningAreaRegions = item1.WarningAreaRegions;
                                }


                                if(item2.isSea == null && item1.isSea != null)
                                {
                                    earthquakeList.items.Last().isSea = item1.isSea;
                                }

                                if (item2.isPLUM == null && item1.isPLUM != null)
                                {
                                    earthquakeList.items.Last().isPLUM = item1.isPLUM;
                                }
                            }


                            find = true;
                            break;
                        }
                        index++;
                    }

                    //見つからなかったら
                    if (!find)
                    {
                        //追加処理(情報更新無し)
                        earthquakeList.items.Add(item1);
                    }
                }

                //追加されていないearthquake2を追加
                int index_temp = 0;
                foreach(var item2 in earthquake2.items)
                {
                    if (!earthquake2_Added.Contains(index_temp))//すでに追加されていない場合
                    {
                        earthquakeList.items.Add(item2);
                    }

                    index_temp++;
                }

                return earthquakeList;


            }
            
        }

        /// <summary>
        /// Delete Earthquake over time
        /// </summary>
        /// <param name="earthquake">EarthquakeList</param>
        /// <param name="referencetime">ReferenceTime</param>
        /// <returns>EarthquakeList.</returns>
        public EarthquakeList? SortingEarthquake(EarthquakeList? earthquake, DateTime referencetime)
        {
            if(earthquake == null || earthquake.items == null)
            {
                return null;
            }
            int index = 0;
            while (index < earthquake.items.Count)
            {
                if(earthquake.items[index].reportTime.Ticks < referencetime.Ticks)//指定の時刻をすぎた場合
                {
                    earthquake.items.RemoveAt(index);
                }
                else
                {
                    index++;
                }
                
            }
            return earthquake;
        }
    }


    public class Item
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public DateTime reportTime { get; set; }
        public string? regionCode { get; set; }
        public string? regionName { get; set; }
        public bool? isCancel { get; set; }
        public int? depth { get; set; }
        public string? calcintensity { get; set; }
        public bool? isFinal { get; set; }
        public DateTime originTime { get; set; }
        public double? magnitude { get; set; }
        public int? Serial { get; set; }
        public string? EventId { get; set; }
        public bool? isSea { get; set; }
        public bool? isWarning { get; set; }
        public bool? isPLUM { get; set; }
        public List<string>? WarningAreaRegions { get; set; }
        public List<string>? WarningAreaDistrict { get; set; }
    }

    public class EarthquakeList
    {
        public List<Item>? items { get; set; }
        public string? LastAction { get; set; }
    }

}