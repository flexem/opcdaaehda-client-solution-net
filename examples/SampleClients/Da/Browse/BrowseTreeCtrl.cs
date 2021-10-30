#region Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved
//-----------------------------------------------------------------------------
// Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved
// Web: https://technosoftware.com  
// 
// Purpose: 
// 
//
// The Software is subject to the Technosoftware GmbH Source Code License Agreement, 
// which can be found here:
// https://technosoftware.com/documents/Source_License_Agreement.pdf
// 
// The Software is based on the OPC .NET API Sample Code.
//-----------------------------------------------------------------------------
#endregion Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved

#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Technosoftware.DaAeHdaClient;
using SampleClients.Common;

using Technosoftware.DaAeHdaClient.Cpx;
using Technosoftware.DaAeHdaClient.Da;

#endregion

namespace SampleClients.Da.Browse
{
	/// <summary>
	/// Use to receive notifications when a server node is 'picked'.
	/// </summary>
	public delegate void ServerPickedEventHandler(TsCDaServer server);

	/// <summary>
	/// Use to receive notifications when a item node is 'picked'.
	/// </summary>
	public delegate void ItemPickedEventHandler(OpcItem itemId);
	
	/// <summary>
	/// Use to receive notifications when a tree node is selected.
	/// </summary>
	public delegate void ElementSelectedEventHandler(TsCDaBrowseElement element);

	/// <summary>
	/// A tree control use to navigate the address space of an OPC DA server.
	/// </summary>
	public class BrowseTreeCtrl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TreeView browseTv_;
		private System.Windows.Forms.ContextMenuStrip popupMenu_;
		private System.Windows.Forms.ToolStripMenuItem connectMi_;
		private System.Windows.Forms.ToolStripMenuItem disconnectMi_;
		private System.Windows.Forms.ToolStripMenuItem refreshMi_;
		private System.Windows.Forms.ToolStripMenuItem editFiltersMi_;
		private System.Windows.Forms.ToolStripMenuItem separator02_;
		private System.Windows.Forms.ToolStripMenuItem setLoginMi_;
		private System.Windows.Forms.ToolStripMenuItem separator01_;
		private System.Windows.Forms.ToolStripMenuItem pickMi_;
		private System.Windows.Forms.ToolStripMenuItem pickChildrenMi_;
		private System.Windows.Forms.ToolStripMenuItem menuItem1_;
		private System.Windows.Forms.ToolStripMenuItem viewComplexTypeMi_;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components_ = null;

		public BrowseTreeCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			browseTv_.ImageList = Resources.Instance.ImageList;
			Clear();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				// release all server objects.
				Clear();

				if(components_ != null)
				{
					components_.Dispose();
				}
			}
			
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.browseTv_ = new System.Windows.Forms.TreeView();
			this.popupMenu_ = new System.Windows.Forms.ContextMenuStrip();
			this.pickMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.pickChildrenMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.separator01_ = new System.Windows.Forms.ToolStripMenuItem();
			this.editFiltersMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.separator02_ = new System.Windows.Forms.ToolStripMenuItem();
			this.setLoginMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.connectMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.disconnectMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem1_ = new System.Windows.Forms.ToolStripMenuItem();
			this.viewComplexTypeMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.SuspendLayout();
			// 
			// BrowseTV
			// 
			this.browseTv_.ContextMenuStrip = this.popupMenu_;
			this.browseTv_.Dock = System.Windows.Forms.DockStyle.Fill;
			this.browseTv_.ImageIndex = -1;
			this.browseTv_.Location = new System.Drawing.Point(0, 0);
			this.browseTv_.Name = "browseTv_";
			this.browseTv_.SelectedImageIndex = -1;
			this.browseTv_.Size = new System.Drawing.Size(400, 400);
			this.browseTv_.TabIndex = 0;
			this.browseTv_.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrowseTV_MouseDown);
			this.browseTv_.DoubleClick += new System.EventHandler(this.PickMI_Click);
			this.browseTv_.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.BrowseTV_AfterSelect);
			this.browseTv_.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.BrowseTV_BeforeExpand);
			// 
			// PopupMenu
			// 
			this.popupMenu_.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
																					  this.pickMi_,
																					  this.pickChildrenMi_,
																					  this.separator01_,
																					  this.editFiltersMi_,
																					  this.refreshMi_,
																					  this.separator02_,
																					  this.setLoginMi_,
																					  this.connectMi_,
																					  this.disconnectMi_,
																					  this.menuItem1_,
																					  this.viewComplexTypeMi_});
			// 
			// PickMI
			// 
			this.pickMi_.ImageIndex = 0;
			this.pickMi_.Text = "&Select";
			this.pickMi_.Click += new System.EventHandler(this.PickMI_Click);
			// 
			// PickChildrenMI
			// 
			this.pickChildrenMi_.ImageIndex = 1;
			this.pickChildrenMi_.Text = "Select Chil&dren";
			this.pickChildrenMi_.Click += new System.EventHandler(this.PickChildrenMI_Click);
			// 
			// Separator01
			// 
			this.separator01_.ImageIndex = 2;
			this.separator01_.Text = "-";
			// 
			// EditFiltersMI
			// 
			this.editFiltersMi_.ImageIndex = 3;
			this.editFiltersMi_.Text = "Set &Filters...";
			this.editFiltersMi_.Click += new System.EventHandler(this.EditFiltersMI_Click);
			// 
			// RefreshMI
			// 
			this.refreshMi_.ImageIndex = 4;
			this.refreshMi_.Text = "&Refresh";
			// 
			// Separator02
			// 
			this.separator02_.ImageIndex = 5;
			this.separator02_.Text = "-";
			// 
			// SetLoginMI
			// 
			this.setLoginMi_.ImageIndex = 6;
			this.setLoginMi_.Text = "Set &Login...";
			this.setLoginMi_.Click += new System.EventHandler(this.SetLoginMI_Click);
			// 
			// ConnectMI
			// 
			this.connectMi_.ImageIndex = 7;
			this.connectMi_.Text = "&Connect...";
			this.connectMi_.Click += new System.EventHandler(this.ConnectMI_Click);
			// 
			// DisconnectMI
			// 
			this.disconnectMi_.ImageIndex = 8;
			this.disconnectMi_.Text = "&Disconnect";
			this.disconnectMi_.Click += new System.EventHandler(this.DisconnectMI_Click);
			// 
			// menuItem1
			// 
			this.menuItem1_.ImageIndex = 9;
			this.menuItem1_.Text = "-";
			// 
			// ViewComplexTypeMI
			// 
			this.viewComplexTypeMi_.ImageIndex = 10;
			this.viewComplexTypeMi_.Text = "&View Complex Type...";
			this.viewComplexTypeMi_.Click += new System.EventHandler(this.ViewComplexTypeMI_Click);
			// 
			// BrowseTreeCtrl
			// 
			this.Controls.Add(this.browseTv_);
			this.Name = "BrowseTreeCtrl";
			this.Size = new System.Drawing.Size(400, 400);
			this.ResumeLayout(false);

		}
		#endregion
				
		/// <summary>
		/// The underlying tree view. 
		/// </summary>
		public TreeView View {get{return browseTv_;}}

		/// <summary>
		/// The server associated with the currently selected node.
		/// </summary>
		public TsCDaServer SelectedServer 
		{
			get
			{ 
				TsCDaServer server = FindServer(browseTv_.SelectedNode); 

				if (server != null)
				{
					return (TsCDaServer)server.Duplicate();
				}

				return null;
			}
		}

		/// <summary>
		/// The currently selected item.
		/// </summary>
		public OpcItem SelectedItem 
		{
			get
			{			
				TreeNode node = browseTv_.SelectedNode;

				if (IsBrowseElementNode(node))
				{
					TsCDaBrowseElement element = (TsCDaBrowseElement)node.Tag;

					if (element.IsItem)
					{
						return new OpcItem(element.ItemPath, element.ItemName);
					}
				}

				if (IsItemPropertyNode(node))
				{
					TsCDaItemProperty property = (TsCDaItemProperty)node.Tag;

					if (property.ItemName != null && ItemPicked != null)
					{
						return new OpcItem(property.ItemPath, property.ItemName);
					}
				}

				return null;
			}
		}
		
		/// <summary>
		/// The connectData associated with the currently selected node.
		/// </summary>
		public OpcConnectData SelectedConnectData {get{ return FindConnectData(browseTv_.SelectedNode); }}

		/// <summary>
		/// Use to receive notifications when a server node is 'picked'.
		/// </summary>
		public event ServerPickedEventHandler ServerPicked;

		/// <summary>
		/// Use to receive notifications when a item node is 'picked'.
		/// </summary>
		public event ItemPickedEventHandler ItemPicked;

		/// <summary>
		/// Use to receive notifications when an element is selected.
		/// </summary>
		public event ElementSelectedEventHandler ElementSelected;

		/// <summary>
		/// The specification of the servers being displayed in the control.
		/// </summary>
		private OpcSpecification mSpecification_;

		/// <summary>
		/// The current filters to apply when expanding nodes.
		/// </summary>
		private TsCDaBrowseFilters mFilters_ = null;

		/// <summary>
		/// References to well-known root nodes.
		/// </summary>
		private TreeNode mLocalServers_ = null;
		private TreeNode mLocalNetwork_ = null;
		private TreeNode mSingleServer_ = null;

		/// <summary>
		/// Displays all servers that support the specified specification.
		/// </summary>
		public void ShowAllServers(OpcSpecification specification, TsCDaBrowseFilters filters)
		{
			Clear();

			mSpecification_ = specification;
			mFilters_       = (filters == null)?new TsCDaBrowseFilters():filters;
	
			browseTv_.ContextMenuStrip = popupMenu_;

			mLocalServers_                    = new TreeNode("Local Servers");
			mLocalServers_.ImageIndex         = Resources.IMAGE_LOCAL_COMPUTER;
			mLocalServers_.SelectedImageIndex = Resources.IMAGE_LOCAL_COMPUTER;
			mLocalServers_.Tag                = null;		

			BrowseServers(mLocalServers_);
			browseTv_.Nodes.Add(mLocalServers_);

			mLocalNetwork_                    = new TreeNode("Local Network");
			mLocalNetwork_.ImageIndex         = Resources.IMAGE_LOCAL_NETWORK;
			mLocalNetwork_.SelectedImageIndex = Resources.IMAGE_LOCAL_NETWORK;
			mLocalNetwork_.Tag                = null;

			BrowseNetwork(mLocalNetwork_);
			browseTv_.Nodes.Add(mLocalNetwork_);
		}

		/// <summary>
		/// Browses the address space for a single server.
		/// </summary>
		public void ShowSingleServer(TsCDaServer server, TsCDaBrowseFilters filters)
		{
			if (server == null) throw new ArgumentNullException("server");

			Clear();

			mFilters_   = (filters == null)?new TsCDaBrowseFilters():filters;

			browseTv_.ContextMenuStrip = popupMenu_;

            mSingleServer_ = new TreeNode(server.ServerName);
			mSingleServer_.ImageIndex         = Resources.IMAGE_LOCAL_SERVER;
			mSingleServer_.SelectedImageIndex = Resources.IMAGE_LOCAL_SERVER;
			mSingleServer_.Tag                = server.Duplicate();

			Connect(mSingleServer_);
			browseTv_.Nodes.Add(mSingleServer_);	
		}

		/// <summary>
		/// Connects to the server and browses for top level nodes.
		/// </summary>
		private void Connect(TreeNode node)
		{
			try
			{
				if (!IsServerNode(node)) return;

				// get the server for the current node.
				TsCDaServer server = (TsCDaServer)node.Tag;

				// connect to server if not already connected.
				if (!server.IsConnected)
				{
					server.Connect(FindConnectData(node));
				}

				// browse for top level elements.
				Browse(node);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Disconnects from the server and clear all children.
		/// </summary>
		private void Disconnect(TreeNode node)
		{
			try
			{
				if (!IsServerNode(node)) return;

				// get the server for the current node.
				TsCDaServer server = (TsCDaServer)node.Tag;

				// connect to server if not already connected.
				if (server.IsConnected)
				{
					server.Disconnect();
				}
				
				node.Nodes.Clear();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Finds the network connectData for the specified node.
		/// </summary>
		private OpcConnectData FindConnectData(TreeNode node)
		{
			if (node != null)
			{
				if (node.Tag != null && node.Tag.GetType() == typeof(OpcConnectData))
				{
					return (OpcConnectData)node.Tag;
				}

				return FindConnectData(node.Parent);
			}

			if (browseTv_.Tag != null && browseTv_.Tag.GetType() == typeof(OpcConnectData))
			{
				return (OpcConnectData)browseTv_.Tag;
			}

			return null;
		}

		/// <summary>
		/// Browses for computers on the network.
		/// </summary>
		private void BrowseNetwork(TreeNode node)
		{
			try
			{
				// clear current contents.
				node.Nodes.Clear();

                IOpcDiscovery discovery = new Technosoftware.DaAeHdaClient.Com.ServerEnumerator();
                string[] hosts = discovery.EnumerateHosts();

				// add children.
				if (hosts != null)
				{
					foreach (string host in hosts)
					{
						TreeNode child = new TreeNode(host);
						child.ImageIndex = child.SelectedImageIndex = Resources.IMAGE_LOCAL_COMPUTER;	
						child.Tag = null;
						child.Nodes.Add(new TreeNode());

						node.Nodes.Add(child);
					}
					
					node.Expand();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Browses for servers a computer.
		/// </summary>
		private void BrowseServers(TreeNode node)
		{
			try
			{
				// clear current contents.
				node.Nodes.Clear();

				// get the host name.
				string host = null;

				if (node != mLocalServers_)
				{
					host = node.Text;
				}

				// get default login information.
				OpcConnectData connectData = FindConnectData(node);

				// find the servers.
				List<OpcServer> servers; 

				OpcUserIdentity userIdentity;
				if (connectData == null || connectData.UserIdentity == null)
				{
					userIdentity = null;
				}
				else
				{
					userIdentity = connectData.UserIdentity;
				}
                // find the servers.
                IOpcDiscovery discovery = new Technosoftware.DaAeHdaClient.Com.ServerEnumerator();
                servers = discovery.GetAvailableServers(mSpecification_, host, connectData).ToList();
				// add children.
				if (servers != null)
				{
					foreach (OpcServer server in servers)
					{
                        TreeNode child = new TreeNode(server.ServerName);
						child.ImageIndex = child.SelectedImageIndex = Resources.IMAGE_LOCAL_SERVER;	
						child.Tag        = server;

						node.Nodes.Add(child);
					}
					
					node.Expand();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Browses for children of the element at the current node.
		/// </summary>
		private void Browse(TreeNode node)
		{
			try
			{
				// get the server for the current node.
				TsCDaServer server = FindServer(node);

				// get the current element to use for a browse.
				TsCDaBrowseElement  parent = null;
				OpcItem itemId = null;

				if (node.Tag != null && node.Tag.GetType() == typeof(TsCDaBrowseElement))
				{
					parent = (TsCDaBrowseElement)node.Tag;
					itemId = new OpcItem(parent.ItemPath, parent.ItemName);
				}

				// clear the node children.
				node.Nodes.Clear();

				// add properties
				if (parent != null && parent.Properties != null)
				{
					foreach (TsCDaItemProperty property in parent.Properties)
					{
						AddItemProperty(node, property);
					}
				}

				// begin a browse.
				Technosoftware.DaAeHdaClient.Da.TsCDaBrowsePosition position = null;
				TsCDaBrowseElement[] elements = server.Browse(itemId, mFilters_, out position);

 				// add children.
				if (elements != null)
				{
					foreach (TsCDaBrowseElement element in elements)
					{
						AddBrowseElement(node, element);
					}
					
					node.Expand();
				}

				// loop until all elements have been fetched.
				while (position != null)
				{
					DialogResult result = MessageBox.Show(
						"More items meeting search criteria exist. Continue browse?", 
						"Browse Items", 
						MessageBoxButtons.YesNo);
					
					if (result == DialogResult.No)
					{
						break;
					}

					// fetch next batch of elements,.
					elements = server.BrowseNext(ref position);
				
					// add children.
					if (elements != null)
					{
						foreach (TsCDaBrowseElement element in elements)
						{
							AddBrowseElement(node, element);
						}

						node.Expand();
					}
				}

				// send notification that property list changed.
				if (ElementSelected != null)
				{
					if (node.Tag.GetType() == typeof(TsCDaBrowseElement))
					{
						ElementSelected((TsCDaBrowseElement)node.Tag);
					}
					else
					{
						ElementSelected(null);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Browses for children of the element at the current node.
		/// </summary>
		private void GetProperties(TreeNode node)
		{
			try
			{
				// get the server for the current node.
				TsCDaServer server = FindServer(node);

				// get the current element to use for a get properties.
				TsCDaBrowseElement element = null;

				if (node.Tag != null && node.Tag.GetType() == typeof(TsCDaBrowseElement))
				{
					element = (TsCDaBrowseElement)node.Tag;
				}

				// can only get properties for an item.
				if (!element.IsItem)
				{
					return;
				}
				
				// clear the node children.
				node.Nodes.Clear();

				// begin a browse.			
				OpcItem itemId = new OpcItem(element.ItemPath, element.ItemName);

				TsCDaItemPropertyCollection[] propertyLists = server.GetProperties(
					new OpcItem[] { itemId },
					mFilters_.PropertyIDs,
					mFilters_.ReturnPropertyValues);

				if (propertyLists != null)
				{
					foreach (TsCDaItemPropertyCollection propertyList in propertyLists)
					{
						foreach (TsCDaItemProperty property in propertyList)
						{
							AddItemProperty(node, property);
						}

						// update element properties.
						element.Properties = (TsCDaItemProperty[])propertyList.ToArray(typeof(TsCDaItemProperty));
					}
				}

				node.Expand();

				// send notification that property list changed.
				if (ElementSelected != null)
				{
					ElementSelected(element);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Checks if the current node is a host or network node.
		/// </summary>
		private bool IsHostNode(TreeNode node)
		{
			if (node == null) return false;
			if (node == mLocalServers_ || node == mSingleServer_) return true;
			if (node.Parent == mLocalNetwork_) return true;
			return false;
		}

		/// <summary>
		/// Checks if the current node is a server node.
		/// </summary>
		private bool IsServerNode(TreeNode node)
		{
			if (node == null ||node.Tag == null) return false;
			return typeof(TsCDaServer).IsInstanceOfType(node.Tag);
		}

		/// <summary>
		/// Checks if the current node is a browse element node.
		/// </summary>
		private bool IsBrowseElementNode(TreeNode node)
		{
			if (node == null || node.Tag == null) return false;
			return (node.Tag.GetType() == typeof(TsCDaBrowseElement));
		}

		/// <summary>
		/// Checks if the current node is an item property node.
		/// </summary>
		private bool IsItemPropertyNode(TreeNode node)
		{
			if (node == null || node.Tag == null) return false;
			return (node.Tag.GetType() == typeof(TsCDaItemProperty));
		}

		/// <summary>
		/// Finds the server for the specified node.
		/// </summary>
		private TsCDaServer FindServer(TreeNode node)
		{
			if (node != null)
			{
				if (IsServerNode(node))
				{
					return (TsCDaServer)node.Tag;
				}

				return FindServer(node.Parent);
			}

			return null;
		}

		/// <summary>
		/// Sends a server or item pciked depending on the node.
		/// </summary>
		private void PickNode(TreeNode node)
		{
			if (IsServerNode(node))
			{
				if (ServerPicked != null)
				{
					ServerPicked((TsCDaServer)node.Tag);
				}
			}

			else if (IsBrowseElementNode(node))
			{
				TsCDaBrowseElement element = (TsCDaBrowseElement)node.Tag;

				if (element.IsItem && ItemPicked != null)
				{
					ItemPicked(new OpcItem(element.ItemPath, element.ItemName));
				}
			}

			else if (IsItemPropertyNode(node))
			{
				TsCDaItemProperty property = (TsCDaItemProperty)node.Tag;

				if (property.ItemName != null && ItemPicked != null)
				{
					ItemPicked(new OpcItem(property.ItemPath, property.ItemName));
				}
			}
		}

		/// <summary>
		/// Displays a dialog with the complex type information.
		/// </summary>
		private void ViewComplexType(TreeNode node)
		{
			if (!IsBrowseElementNode(node))
			{
				return;
			}

			try
			{
				TsCCpxComplexItem complexItem = TsCCpxComplexTypeCache.GetComplexItem((TsCDaBrowseElement)node.Tag);

				if (complexItem != null)
				{
					new EditComplexValueDlg().ShowDialog(complexItem, null, true, true);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
        }

		/// <summary>
		/// Adds the specified browse element into the tree.
		/// </summary>
		private void AddBrowseElement(TreeNode parent, TsCDaBrowseElement element)
		{
			// create the new node.
			TreeNode node = new TreeNode(element.Name);
			
			// select the icon.
			if (element.IsItem)
			{
				node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_GREEN_SCROLL;
			}
			else
			{
				node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_CLOSED_YELLOW_FOLDER;
			}
	
			node.Tag = element;

			// add a dummy node to force display of '+' symbol.
			if (element.HasChildren)
			{
				node.Nodes.Add(new TreeNode());
			}

			// add properties
			if (element.Properties != null)
			{
				foreach (TsCDaItemProperty property in element.Properties)
				{
					AddItemProperty(node, property);
				}
			}

			// add to parent.
			parent.Nodes.Add(node);
		}

		/// <summary>
		/// Adds the specified item property into the tree.
		/// </summary>
		private void AddItemProperty(TreeNode parent, TsCDaItemProperty property)
        {
            if (property != null && property.Result.Succeeded())
            {
                // create the new node.
                TreeNode node = new TreeNode(property.Description);

                // select the icon.
                if (property.ItemName != null && property.ItemName != "")
                {
                    node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_GREEN_SCROLL;
                }
                else
                {
                    node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_EXPLODING_BOX;
                }

                node.Tag = property;

                if (property.Value != null)
                {
                    TreeNode child = new TreeNode(Technosoftware.DaAeHdaClient.OpcConvert.ToString(property.Value));
                    child.ImageIndex = child.SelectedImageIndex = Resources.IMAGE_LIST_BOX;
                    child.Tag = property.Value;
                    node.Nodes.Add(child);

                    if (property.Value.GetType().IsArray)
                    {
                        foreach (object element in (Array)property.Value)
                        {
                            TreeNode arrayChild = new TreeNode(Technosoftware.DaAeHdaClient.OpcConvert.ToString(element));
                            arrayChild.ImageIndex = arrayChild.SelectedImageIndex = Resources.IMAGE_LIST_BOX;
                            arrayChild.Tag = element;
                            child.Nodes.Add(arrayChild);
                        }
                    }
                }

                // add to parent.
                parent.Nodes.Add(node);
            }
		}

		/// <summary>
		/// Removes all nodes and releases all resources.
		/// </summary>
		public void Clear()
		{		
			// recursively searches the tree and free objects.
			foreach (TreeNode child in browseTv_.Nodes)
			{
				Clear(child);
			}

			// clear the tree.
			browseTv_.Nodes.Clear();

			mLocalServers_ = null;
			mLocalNetwork_ = null;
			mSingleServer_ = null;
		}

		/// <summary>
		/// Recursively searches the tree and free objects.
		/// </summary>
		private void Clear(TreeNode parent)
		{		
			// search children.
			foreach (TreeNode child in parent.Nodes)
			{
				Clear(child);
			}

			// disconnect servers.
			if (IsServerNode(parent))
			{
				TsCDaServer server = (TsCDaServer)parent.Tag;
				if (server.IsConnected) server.Disconnect();
			}
		}

		/// <summary>
		/// Called before a node is about to expand.
		/// </summary>
		private void BrowseTV_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;

			if (node != null && node.Parent == mLocalNetwork_)
			{
				// browse server if not already done.
				if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
				{
					BrowseServers(node);
				}
			}

			if (IsServerNode(node))
			{
				// connect to server if not already connected.
				if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
				{
					Connect(node);
				}

				return;
			}

			if (IsBrowseElementNode(node))
			{
				// browse for children if not already fetched.
				if (node.Nodes.Count >= 1 && node.Nodes[0].Text == "")
				{
					Browse(node);
				}

				return;
			}
		}

		/// <summary>
		/// Updates the state of context menus based on the current selection.
		/// </summary>
		private void BrowseTV_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// ignore left button actions.
			if (e.Button != MouseButtons.Right)	return;

			// selects the item that was right clicked on.
			TreeNode clickedNode = browseTv_.GetNodeAt(e.X, e.Y);

			// no item clicked on - do nothing.
			if (clickedNode == null) return;

			// force selection to clicked node.
			browseTv_.SelectedNode = clickedNode;

			// disable everything.
			pickMi_.Enabled            = false;
			editFiltersMi_.Enabled     = false;
			refreshMi_.Enabled         = false;
			setLoginMi_.Enabled        = false;
			connectMi_.Enabled         = false;
			disconnectMi_.Enabled      = false;
			viewComplexTypeMi_.Enabled = false;

			if (clickedNode == mLocalNetwork_ || IsHostNode(clickedNode))
			{
				refreshMi_.Enabled  = true;
				setLoginMi_.Enabled = true;
				return;
			}

			if (IsServerNode(clickedNode))
			{
				pickMi_.Enabled        = true;
				connectMi_.Enabled     = !((TsCDaServer)clickedNode.Tag).IsConnected;
				disconnectMi_.Enabled  = ((TsCDaServer)clickedNode.Tag).IsConnected;
				editFiltersMi_.Enabled = true;
				refreshMi_.Enabled     = true;
				return;
			}

			if (IsBrowseElementNode(clickedNode))
			{
				pickMi_.Enabled            = true;
				editFiltersMi_.Enabled     = true;
				refreshMi_.Enabled         = true;
				viewComplexTypeMi_.Enabled = (TsCCpxComplexTypeCache.GetComplexItem((TsCDaBrowseElement)clickedNode.Tag) != null);
				return;
			}
		}	
		
		/// <summary>
		/// Called when the browse filters have changed.
		/// </summary>
		private void OnBrowseFiltersChanged(TsCDaBrowseFilters filters)
		{
			mFilters_ = filters;

			if (IsBrowseElementNode(browseTv_.SelectedNode))
			{
				TsCDaBrowseElement element = (TsCDaBrowseElement)browseTv_.SelectedNode.Tag;

				if (!element.HasChildren)
				{
					GetProperties(browseTv_.SelectedNode);
					return;
				}
			}

			Browse(browseTv_.SelectedNode);
		}
		
		/// <summary>
		/// Connects to the selected server.
		/// </summary>
		private void ConnectMI_Click(object sender, System.EventArgs e)
		{
			Connect(browseTv_.SelectedNode);		
		}

		/// <summary>
		/// Connects to the selected server.
		/// </summary>
		private void DisconnectMI_Click(object sender, System.EventArgs e)
		{
			Disconnect(browseTv_.SelectedNode);		
		}

		/// <summary>
		/// Displays the edit browse filters dialog.
		/// </summary>
		private void EditFiltersMI_Click(object sender, System.EventArgs e)
		{
			new BrowseFiltersDlg().Show(MainForm.ActiveForm, mFilters_, new BrowseFiltersChangedCallback(OnBrowseFiltersChanged));	
		}
		
		/// <summary>
		/// Updates the contents of the current selection.
		/// </summary>
		private void RefreshMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = browseTv_.SelectedNode;

			if (node == mLocalNetwork_) { BrowseNetwork(node); return; }	
			if (IsHostNode(node))       { BrowseServers(node); return; }

			Browse(node);
		}

		/// <summary>
		/// Prompts the user to set the network connectData for the node.
		/// </summary>
		private void SetLoginMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = browseTv_.SelectedNode;

			if (node == mLocalNetwork_ | IsHostNode(node))       
			{ 
				OpcConnectData connectData = (OpcConnectData)node.Tag;

				if (connectData == null)
				{
					node.Tag = connectData = new OpcConnectData(null);
				}

				connectData.UserIdentity = new NetworkCredentialsDlg().ShowDialog(connectData.UserIdentity);
			}
		}

		/// <summary>
		/// Sends a server or item selected event.
		/// </summary>
		private void PickMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = browseTv_.SelectedNode;

			if (node != null)
			{
				PickNode(node);
			}
		}

		/// <summary>
		/// Sends a server or item selected event for all node children.
		/// </summary>
		private void PickChildrenMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = browseTv_.SelectedNode;
			
			if (node != null)
			{
				foreach (TreeNode child in node.Nodes)
				{
					PickNode(child);
				}
			}
		}

		/// <summary>
		/// Called when a tree node is selected.
		/// </summary>
		private void BrowseTV_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = browseTv_.SelectedNode;

			if (ElementSelected != null)
			{
				if (IsBrowseElementNode(node))
				{
					ElementSelected((TsCDaBrowseElement)node.Tag);
				}
				else
				{
					ElementSelected(null);
				}
			}
		}

		/// <summary>
		/// Called to view complex type information for an item.
		/// </summary>
		private void ViewComplexTypeMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = browseTv_.SelectedNode;

			if (node != null)
			{
				ViewComplexType(node);
			}
		}
	}
}