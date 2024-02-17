namespace CSToolbox
{
	public delegate void TypedEventHandler<TSender>(TSender sender);
	public delegate void TypedEventHandler<TSender, TArgs>(TSender sender, TArgs args);
}
