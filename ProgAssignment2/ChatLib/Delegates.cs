

namespace ChatLib
{
    /// <summary>
    /// handles an incoming message from a separate thread
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e"> message received event args</param>
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
    
}
