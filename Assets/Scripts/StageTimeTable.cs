using System.Globalization;
using System.Xml;
using UnityEngine;

public class StageTimeTable : MonoBehaviour
{
	public const int ITEM_COUNT_MAX = 7;

	public const int TBL_COUNT_MAX = 1;

	[SerializeField]
	private TextAsset m_stageTimeTable;

	private static readonly string[] ITEM_NAMES = new string[7]
	{
		"StartTime",
		"OverlapBonus",
		"ItemExtendedLimit",
		"BronzeWatch",
		"SilverWatch",
		"GoldWatch",
		"Continue"
	};

	private int[] m_tblInfo;

	private int m_tblCount;

	private void Start()
	{
		if (m_tblInfo == null)
		{
			m_tblInfo = new int[7];
		}
		if (m_stageTimeTable != null && (bool)m_stageTimeTable)
		{
			string xml = AESCrypt.Decrypt(m_stageTimeTable.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			CreateTable(xmlDocument, m_tblInfo, out m_tblCount);
			if (m_tblCount != 0)
			{
			}
		}
	}

	public static string GetItemName(uint index)
	{
		if (index < 7 && index < ITEM_NAMES.Length)
		{
			return ITEM_NAMES[index];
		}
		return string.Empty;
	}

	public int GetTableItemData(StageTimeTableItem item)
	{
		return GetData((int)item);
	}

	public static void CreateTable(XmlDocument doc, int[] data, out int tbl_count)
	{
		tbl_count = 0;
		if (doc == null || doc.DocumentElement == null)
		{
			return;
		}
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("StageTimeTable");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (XmlNode item in xmlNodeList)
		{
			XmlNodeList xmlNodeList2 = item.SelectNodes("Item");
			foreach (XmlNode item2 in xmlNodeList2)
			{
				for (int i = 0; i < 7; i++)
				{
					string itemName = GetItemName((uint)i);
					XmlAttribute xmlAttribute = item2.Attributes[itemName];
					int num2 = 0;
					if (xmlAttribute != null)
					{
						num2 = int.Parse(item2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
					}
					int num3 = num * 7 + i;
					data[num3] = num2;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	public bool IsSetupEnd()
	{
		if (m_tblInfo == null)
		{
			return false;
		}
		return true;
	}

	private int GetData(int tbl_index, int item_index)
	{
		if (m_tblInfo != null && (uint)tbl_index < m_tblCount)
		{
			int num = tbl_index * 7 + item_index;
			if (num < m_tblInfo.Length)
			{
				return m_tblInfo[num];
			}
		}
		return 0;
	}

	private int GetData(int index)
	{
		if (m_tblInfo != null && index < m_tblInfo.Length)
		{
			return m_tblInfo[index];
		}
		return 0;
	}
}