namespace WarChess
{
    /// <summary>
    /// The way to target
    /// </summary>
    public enum ESkillTargetWay
    {
        /// <summary>
        /// target under mouse pointer
        /// </summary>
        Single,
        /// <summary>
        /// all targets in the skill area
        /// </summary>
        All,
        /// <summary>
        /// all ally in the skill area
        /// </summary>
        Ally,
        /// <summary>
        /// all opponent in the skill area
        /// </summary>
        Opponent,
    }
}