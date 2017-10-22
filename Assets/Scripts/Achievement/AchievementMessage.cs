using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMessage : MonoBehaviour {
	Image targetimage;
	Text targettext;

	// Use this for initialization
	void Start ()
	{
		targetimage = GetComponent<Image> ();
		targettext = GetComponentInChildren<Text> ();
		Close ();
	}

	public void Open(int id)
	{
		targetimage.enabled = true;
		int rank = AchievementManager.Instance.rank [id];
		int count = AchievementManager.Instance.count[id];
		int nextcount = AchievementManager.Instance.nextCount (id, AchievementManager.Instance.rank [id]);
		switch (id)
		{
		case 0:
			targettext.text = string.Format ("Slimeを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			case 2:
				targettext.text += "戦い方が わかってきた みてえだな その調子で 経験を 積んで もっと レベルを あげるんだ";
				break;
			case 3:
				targettext.text += "戦い方は もう バッチリ みてえだな あんたなら もっと 上を 目指せるぜ";
				break;
			case 4:
				targettext.text += "スライム退治 といえば あんただ 街中で 噂されてるぜ";
				break;
			default:
				targettext.text += "魔物との 戦い方が わからない？ 右手の 剣アイコンを タップする だけだぜ";
				break;
			}
			break;
		case 1:
			targettext.text = string.Format("Ratを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "すばしっこくて ちっとばかし 厄介な やつかもな Hit Potionを 持っておくと いいぞ";
				break;
			}
			break;
		case 2:
			targettext.text = string.Format("Hornetを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "スズメバチに 気をつけろ 並の 一撃じゃ カスリもしねえ って話だ";
				break;
			}
			break;
		case 3:
			targettext.text = string.Format("Zombieを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "Atk Potionを とっておけ 奴ら もう 死んでるからか 何度斬っても しつこく 襲ってくるぞ";
				break;
			}
			break;
		case 4:
			targettext.text = string.Format("Skeletonを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "亡霊ってのは 恐ろしいもんだ 斬りつけても まるで 手応えがないってよ";
				break;
			}
			break;
		case 5:
			targettext.text = string.Format("Dragonewtを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "龍だか トカゲだか よくわかんねえんだが やつらに 用心しろ スキらしい スキが 見当たらねえんだ";
				break;
			}
			break;
		case 6:
			targettext.text = string.Format("Taurusを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "牛アタマの デカブツに スキを見せるな 当たりどころが 悪けりゃ 一撃で ノックアウトだ";
				break;
			}
			break;
		case 7:
			targettext.text = string.Format("Demonを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "熟練の 冒険者も 手こずる 恐ろしい 怪物だ ハンパな 装備で 挑んだら 返り討ちに されちまうぜ";
				break;
			}
			break;
		case 8:
			targettext.text = string.Format("Phantomを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "でっかい鎌が 目印だ 目があっちまう前に 逃げろ 捕まっちまったら 一巻の終わり ってやつだ";
				break;
			}
			break;
		case 9:
			targettext.text = string.Format("Dragonを 倒した数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			default:
				targettext.text += "皆 やつに挑んで 帰れなくなった 人間ごときが どうにかできる 生き物じゃねえ ってことだ";
				break;
			}
			break;
		case 10:
			targettext.text = string.Format("使ったGold:{0,7}/{1,7}\n", count, nextcount);
			switch(rank)
			{
//			case 2:
//				targettext.text += "金が 足りねえって 時に 何から 買うか 考えるのも 冒険者の 仕事って やつだぜ";
//				break;
//			case 3:
//				targettext.text += "";
//				break;
			default:
				targettext.text += "武器屋 防具屋 薬屋を 回って 役立つ 道具を 揃えておくんだ 備えあれば憂いなし ってな";
				break;
			}
			break;
		case 11:
			targettext.text = string.Format("歩数:{0,7}/{1,7}\n", count, nextcount);
			switch(rank)
			{
//			case 2:
//				targettext.text += "だいぶ 歩き慣れた みてえだな ";
//				break;
//			case 3:
//				targettext.text += "相当 歩き回ってる みてえだな";
//				break;
			default:
				targettext.text += "ダンジョンの 進み方が わからない？ 画面を タップしたり スワイプしてみると いいぜ";
				break;
			}
			break;
		case 12:
			targettext.text = string.Format ("到達階数:{0,2}/{1,2}\n", count, nextcount);
			switch(rank)
			{
			case 2:
				targettext.text += "9Fまで たどり着けるとは なかなか やるねえ 仕掛けには せいぜい 気をつけな";
				break;
			case 3:
				targettext.text += "17Fまで たどり着ける 冒険者は なかなかいない あんた ただもんじゃ ねえな";
				break;
			case 4:
				targettext.text += "20Fまで たどり着いた 冒険者は 久々だ あんたの ことが 街中で 噂になってるぜ";
				break;
			default:
				targettext.text += "下りはしごを 探すんだ 奥へ 進むほど お宝が たくさん 見つかるって 話だぜ";
				break;
			}
			break;
		case 13:
			targettext.text = string.Format("宝箱を 見つけた数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank){
			case 2:
				targettext.text += "お宝も そこそこ 見つけてきた みてえだな 立派な 冒険者に なれるぜ";
				break;
			case 3:
				targettext.text += "だいぶ お宝を 集めてきた みてえだな あんたも 立派な 冒険者だ";
				break;
			case 4:
				targettext.text += "ここまで お宝に 恵まれるとは やるねえ あんたこそ 伝説の 冒険者だ";
				break;
			default:
				targettext.text += "装備を 整えるには 金がいる 宝箱を 見つけたら 忘れずに とっておくんだぜ";
				break;
			}
			break;
		case 14:
			targettext.text = string.Format("罠に かかった数:{0,4}/{1,4}\n", count, nextcount);
			switch(rank)
			{
			case 2:
				targettext.text += "Trap Guardは 持ってないのか？ 罠を 甘く 見てると そのうち 痛い目みるぞ";
				break;
			case 3:
				targettext.text += "ここまでくると ある意味 自慢できるぜ あんたに Trap Guardなんて 似合わねえ";
				break;
			case 4:
				targettext.text += "ここまで 罠に かかり続けたのは あんたが 初めてだ 伝説に 残るぜ";
				break;
			default:
				targettext.text += "見えないように 隠されてて 魔物より 厄介かもな Trap Guardは 忘れるなよ";
				break;
			}
			break;
		case 15:
			targettext.text = string.Format ("Game Overに なった数:{0,4}/{1,4}\n", count, nextcount);
			switch (rank)
			{
			case 2:
				targettext.text += "あんた 随分と 無茶するねえ もっと 慎重に 進んだ方が いいぜ";
				break;
			case 3:
				targettext.text += "100回も 失敗したら 普通は 心が 折れるもんだ あんた クレイジーだ 気に入ったぜ";
				break;
			case 4:
				targettext.text += "あんたの 心の 強さに 感服したぜ あんたこそ 伝説の 勇者に 違いねえ";
				break;
			default:
				targettext.text += "体力が 尽きたら おしまいだからな 危険を 感じたら 上りはしごを 目指すんだ";
				break;
			}
			break;
		default:
			break;
		}
		AchievementManager.Instance.unread [id] = false;
		AudioManager.Instance.playSE (1);
	}

	public void Close()
	{
		targetimage.enabled = false;
		targettext.text = "";
	}
}
