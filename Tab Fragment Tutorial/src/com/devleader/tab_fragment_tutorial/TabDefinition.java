package com.devleader.tab_fragment_tutorial;

import java.util.UUID;

import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

/**
 * A class that defines a UI tab.  
 */
public abstract class TabDefinition {
	//
	// Fields
	//
	private final int _tabContentViewId;
	private final String _tabUuid;
	
	//
	// Constructors
	//
	/**
	 * The constructor for {@link TabDefinition}.
	 * @param tabContentViewId The layout ID of the contents to use when the tab is active.
	 */
	public TabDefinition(int tabContentViewId) {
		_tabContentViewId = tabContentViewId;
		_tabUuid = UUID.randomUUID().toString();
	}
	
	//
	// Exposed Members
	//
	/**
	 * Gets the ID of the tab's content {@link View}.
	 * @return The ID of the tab's content {@link View}.
	 */
	public int getTabContentViewId() {
		return _tabContentViewId;
	}
	
	/**
	 * Gets the unique identifier for the tab.
	 * @return The unique identifier for the tab.
	 */
	public String getId() {
		return _tabUuid;
	}
	
	/**
	 * Gets the {@link Fragment} to use for the tab.
	 * @return The {@link Fragment} to use for the tab.
	 */
	public abstract Fragment getFragment();
	
	/**
	 * Called when creating the {@link View} for the tab control.
	 * @param inflater The {@link LayoutInflater} used to create {@link View}s.
	 * @param tabsView The {@link View} that holds the tab {@link View}s.
	 * @return The tab {@link View} that will be placed into the tabs {@link ViewGroup}.
	 */
	public abstract View createTabView(LayoutInflater inflater, ViewGroup tabsView);
}
