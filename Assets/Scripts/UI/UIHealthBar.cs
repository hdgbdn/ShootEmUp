//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using ShotEmUp;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    [SerializeField]
    private Text m_healthText;
    [SerializeField]
    private Text m_lifes;

    private StringBuilder m_sb;

    private PlayerManager m_playerManager;
    void Start()
    {
        if(m_healthText == null)
        {
            Debug.LogError("m_healthText is null!");
        }
        if (m_lifes == null)
        {
            Debug.LogError("m_healthText is null!");
        }

        m_playerManager = GameManager.GetManager<PlayerManager>();

        m_sb = new StringBuilder();
    }

    private void Update()
    {
        if(m_playerManager != null)
        {
            m_sb.Append(m_playerManager.PlayerCurHealth);
            m_sb.Append("/");
            m_sb.Append(m_playerManager.PlayerMaxHealth);
            m_healthText.text = m_sb.ToString();
            m_sb.Clear();
            m_sb.Append(m_playerManager.PlayerLifes);
            m_lifes.text = m_sb.ToString();
            m_sb.Clear();
        }
    }

}
