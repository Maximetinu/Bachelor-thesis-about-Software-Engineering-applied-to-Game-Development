using UnityEngine;
public class PlayerInput
{
    Vector2 m_axis = Vector2.zero;
    bool m_jump = false, m_jumpDown = false, m_jumpUp = false;
    bool m_primary = false, m_primaryDown = false, m_primaryUp = false;
    bool m_secondary = false, m_secondaryDown = false, m_secondaryUp = false;
    bool m_special = false, m_specialDown = false, m_specialUp = false;
    bool m_pauseDown = false;
    static PlayerInput m_singleInstance = new PlayerInput();

    public Vector2 Axis { get { return m_axis; } }

    public bool Jump { get { return m_jump; } }
    public bool JumpDown { get { return m_jumpDown; } }
    public bool JumpUp { get { return m_jumpUp; } }

    public bool Primary { get { return m_primary; } }
    public bool PrimaryDown { get { return m_primaryDown; } }
    public bool PrimaryUp { get { return m_primaryUp; } }

    public bool Secondary { get { return m_secondary; } }
    public bool SecondaryDown { get { return m_secondaryDown; } }
    public bool SecondaryUp { get { return m_secondaryUp; } }

    public bool Special { get { return m_special; } }
    public bool SpecialDown { get { return m_specialDown; } }
    public bool SpecialUp { get { return m_specialUp; } }

    public bool PauseDown { get { return m_pauseDown; } }

    public static PlayerInput NullInput
    {
        get
        {
            m_singleInstance = new PlayerInput();
            return m_singleInstance;
        }
    }

    // Avoiding memory allocation on every frame
    private PlayerInput()
    {
        m_axis = Vector2.zero;
        m_jump = false;
        m_jumpDown = false;
        m_jumpUp = false;
        m_primary = false;
        m_primaryDown = false;
        m_primaryUp = false;
        m_secondary = false;
        m_secondaryDown = false;
        m_secondaryUp = false;
        m_special = false;
        m_specialDown = false;
        m_specialUp = false;
        m_pauseDown = false;
    }
    public static void NewInput(out PlayerInput recycledInstance, Vector2 axis, bool jump, bool jumpDown, bool jumpUp, bool primary, bool primaryDown, bool primaryUp, bool secondary, bool secondaryDown, bool secondaryUp, bool special, bool specialDown, bool specialUp, bool pauseDown)
    {
        recycledInstance = m_singleInstance;

        recycledInstance.m_axis = axis;

        recycledInstance.m_jump = jump;
        recycledInstance.m_jumpDown = jumpDown;
        recycledInstance.m_jumpUp = jumpUp;

        recycledInstance.m_primary = primary;
        recycledInstance.m_primaryDown = primaryDown;
        recycledInstance.m_primaryUp = primaryUp;

        recycledInstance.m_secondary = secondary;
        recycledInstance.m_secondaryDown = secondaryDown;
        recycledInstance.m_secondaryUp = secondaryUp;

        recycledInstance.m_special = special;
        recycledInstance.m_specialDown = specialDown;
        recycledInstance.m_specialUp = specialUp;

        recycledInstance.m_pauseDown = pauseDown;
    }
}