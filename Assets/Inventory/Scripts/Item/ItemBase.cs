using UnityEngine;
using UnityEngine.VFX;

namespace FlMr_Inventory
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] private int uniqueId;
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;
        [SerializeField] private ArcanaBase arcanaBase = null;
        [SerializeField] private int maxCoolTime = 10;
        private float coolTime = 0;
        private bool activeFlg = true;

        /// <summary>
        /// �A�C�e���̎�ނ�1:1�Ή����鐮��
        /// (�f�[�^��ۑ�����Ƃ���A�����ʐM�@�\����������ۂɐ^���𔭊�����)
        /// </summary>
        public int UniqueId => uniqueId;

        /// <summary>
        /// �A�C�e����
        /// </summary>
        public string ItemName => itemName;

        /// <summary>
        /// �A�C�e���̃A�C�R��
        /// </summary>
        public Sprite Icon => icon;

        /// <summary>
        /// �v���C���[�ɑ΂���A�C�e���̐���
        /// </summary>
        public string Description => description;

        public ArcanaBase GetArcana()
        {
            activeFlg = false;
            coolTime = 0;
            return arcanaBase;
        }
        public void GetCoolTime()
        {
            if (!activeFlg)
            {
                coolTime += Time.deltaTime;
                if (coolTime > maxCoolTime) activeFlg = true;
            }
        }
        public bool GetActiveFlg() { return activeFlg; }

        public float GetNowCoolTime()
        {
            return coolTime;
        }

        public void SetCoolTime(int count)
        {
            coolTime = maxCoolTime - count;
            activeFlg = false;
        }

        public float GetAdjustmentCooltime()
        {
            float adjustmentCooltime = maxCoolTime * 0.1f;
            adjustmentCooltime = coolTime / adjustmentCooltime;
            return adjustmentCooltime;
        }
    }
}